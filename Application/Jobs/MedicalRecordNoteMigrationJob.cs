using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Hangfire.Server;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Models.Masters;

namespace Application.Jobs
{
    public class MedicalRecordNoteMigrationJob
    {
        private readonly IRestAPIService _restAPIService;
        private readonly IUriService _uriService;
        private readonly ILogger<MedicalRecordNoteMigrationJob> _logger;

        public MedicalRecordNoteMigrationJob(
            IRestAPIService restAPIService,
            IUriService uriService,
            ILogger<MedicalRecordNoteMigrationJob> logger)
        {
            _restAPIService = restAPIService;
            _uriService = uriService;
            _logger = logger;
        }

        public async Task ExecuteAsync(string dbName, int batchCount, PerformContext context)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var hangfireJobId = context?.BackgroundJob?.Id ?? "unknown";
            
            _logger.LogInformation($"[MigrationJob] Starting migration job for Database: {dbName}, BatchCount: {batchCount}, JobId: {hangfireJobId}");

            int totalFound = 0;
            int totalProcessed = 0;
            int totalMigrated = 0;
            int totalImagesUploaded = 0;
            int totalFailures = 0;
            var detailsList = new System.Collections.Generic.List<object>();

            try
            {
                // 1. Generate system JWT token representing Superadmin with Entity = dbName
                var systemUser = new Users
                {
                    Id = 0,
                    Name = "System Migrator",
                    Email = "system-migration@vethub.id",
                    Roles = "Superadmin",
                    Entity = dbName,
                    IsVerified = true
                };
                var token = JwtUtil.SetSessionToken(systemUser);
                string authHeader = "Bearer " + token;

                // 2. Fetch notes from Client API
                // URL: api/MedicalRecords/Migration/Notes?batchCount=batchCount
                var notes = await _restAPIService.GetResponse<System.Collections.Generic.IEnumerable<MedicalRecordsNotes>>(
                    APIType.Client, $"MedicalRecords/Migration/Notes?batchCount={batchCount}", authHeader);

                if (notes == null || !notes.Any())
                {
                    _logger.LogInformation($"[MigrationJob] No notes found for migration in database: {dbName}");
                    detailsList.Add(new { status = "NoAction", message = "No legacy base64 image notes found to process." });
                    LogStats(dbName, hangfireJobId, totalFound, totalProcessed, totalMigrated, totalImagesUploaded, totalFailures, stopwatch.Elapsed);
                    return;
                }

                totalFound = notes.Count();

                foreach (var note in notes)
                {
                    totalProcessed++;
                    try
                    {
                        var htmlContent = note.Value;
                        if (string.IsNullOrEmpty(htmlContent))
                        {
                            detailsList.Add(new { noteId = note.Id, status = "Skipped", message = "HTML content is empty." });
                            continue;
                        }

                        // Parse HTML using HtmlAgilityPack
                        var doc = new HtmlDocument();
                        doc.LoadHtml(htmlContent);

                        var imgNodes = doc.DocumentNode.SelectNodes("//img[@src]");
                        if (imgNodes == null || !imgNodes.Any())
                        {
                            detailsList.Add(new { noteId = note.Id, status = "Skipped", message = "No image tags found." });
                            continue;
                        }

                        bool noteUpdated = false;
                        int noteImagesUploaded = 0;

                        foreach (var imgNode in imgNodes)
                        {
                            var srcValue = imgNode.GetAttributeValue("src", "");
                            if (srcValue.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
                            {
                                // base64 image!
                                // Format: data:image/[type];base64,[data]
                                var commaIndex = srcValue.IndexOf(',');
                                if (commaIndex == -1) continue;

                                var base64Data = srcValue.Substring(commaIndex + 1);

                                byte[] imageBytes;
                                try
                                {
                                    imageBytes = Convert.FromBase64String(base64Data);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogWarning($"[MigrationJob] Failed to parse base64 for NoteId: {note.Id}. Error: {ex.Message}");
                                    continue;
                                }

                                var guid = Guid.NewGuid().ToString("N");
                                var fileName = $"{guid}.webp";

                                // Save path: medical-record-notes/{tenantDatabaseName}/{noteId}/{guid}.webp
                                string folder = $"medical-record-notes/{dbName}/{note.Id}";
                                string folderPath = PathHelper.GetUploadPath(UploadPathType.Images, folder);
                                string filePath = Path.Combine(folderPath, fileName);

                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }

                                await File.WriteAllBytesAsync(filePath, imageBytes);

                                // Generate URL
                                var baseUrl = _uriService.GetBaseWebUri().ToString();
                                if (!baseUrl.EndsWith("/")) baseUrl += "/";
                                var generatedUrl = $"{baseUrl}Upload/v/{folder}/{fileName}";

                                // Replace src attribute
                                imgNode.SetAttributeValue("src", generatedUrl);
                                noteImagesUploaded++;
                                totalImagesUploaded++;
                                noteUpdated = true;
                            }
                        }

                        if (noteUpdated)
                        {
                            // Save HTML back using PUT api/MedicalRecords/Migration/Notes/{id}
                            var updatedHtml = doc.DocumentNode.OuterHtml;
                            var requestObj = new UpdateNoteHtmlRequest { HtmlContent = updatedHtml };

                            await _restAPIService.PutResponse<object>(
                                APIType.Client, "MedicalRecords/Migration/Notes", 
                                note.Id, JsonConvert.SerializeObject(requestObj), authHeader);

                            totalMigrated++;
                            detailsList.Add(new { noteId = note.Id, status = "Success", message = $"Migrated {noteImagesUploaded} base64 images." });
                        }
                        else
                        {
                            detailsList.Add(new { noteId = note.Id, status = "Skipped", message = "No base64 images found." });
                        }
                    }
                    catch (Exception ex)
                    {
                        totalFailures++;
                        _logger.LogError(ex, $"[MigrationJob] Failed to migrate note ID {note.Id} in database {dbName}");
                        detailsList.Add(new { noteId = note.Id, status = "Failed", message = ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[MigrationJob] Fatal error during migration for database {dbName}");
                detailsList.Add(new { status = "Failed", message = $"Fatal error during execution: {ex.Message}" });
            }
            finally
            {
                try
                {
                    string logFolder = $"medical-record-notes/{dbName}/logs";
                    string logFolderPath = PathHelper.GetUploadPath(UploadPathType.Images, logFolder);
                    if (!Directory.Exists(logFolderPath))
                    {
                        Directory.CreateDirectory(logFolderPath);
                    }

                    var logData = new
                    {
                        dbname = dbName,
                        batchCount = batchCount,
                        details = detailsList
                    };

                    string jsonLog = JsonConvert.SerializeObject(logData, Formatting.Indented);
                    string logFileName = $"{dbName}_{batchCount}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    string logFilePath = Path.Combine(logFolderPath, logFileName);
                    
                    File.WriteAllText(logFilePath, jsonLog);
                    _logger.LogInformation($"[MigrationJob] Saved migration log to: {logFilePath}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[MigrationJob] Failed to write migration log file.");
                }
            }

            stopwatch.Stop();
            LogStats(dbName, hangfireJobId, totalFound, totalProcessed, totalMigrated, totalImagesUploaded, totalFailures, stopwatch.Elapsed);
        }

        private void LogStats(
            string dbName, 
            string hangfireJobId, 
            int totalFound, 
            int totalProcessed, 
            int totalMigrated, 
            int totalImagesUploaded, 
            int totalFailures, 
            TimeSpan duration)
        {
            _logger.LogInformation(
                $"[MigrationJob] Finished migration." + Environment.NewLine +
                $"- Tenant Database: {dbName}" + Environment.NewLine +
                $"- Hangfire Job ID: {hangfireJobId}" + Environment.NewLine +
                $"- Total Notes Found: {totalFound}" + Environment.NewLine +
                $"- Total Notes Processed: {totalProcessed}" + Environment.NewLine +
                $"- Total Notes Migrated: {totalMigrated}" + Environment.NewLine +
                $"- Total Images Uploaded: {totalImagesUploaded}" + Environment.NewLine +
                $"- Total Failures: {totalFailures}" + Environment.NewLine +
                $"- Execution Duration: {duration.TotalSeconds:F2} seconds"
            );
        }
    }
}
