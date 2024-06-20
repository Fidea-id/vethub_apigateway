using Application.Services.Contracts;
using Application.Utils;
using Domain.Entities;
using Domain.Entities.DTOs.Clients;
using Domain.Entities.Models.Clients;
using Domain.Entities.Requests.Clients;
using Domain.Entities.Responses.Clients;
using Domain.Utils;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Application.Services.Implementations
{
    public class DocGenerateService : IDocGenerateService
    {
        private IUriService _uriService;
        private IRestAPIService _restAPIService;

        public DocGenerateService(IUriService uriService, IRestAPIService restAPIService)
        {
            _uriService = uriService;
            _restAPIService = restAPIService;
        }

        public async Task<DocGenerateResponse> GenerateSuratKematianAsync(string userId, DocsKematianRequest request, string auth)
        {
            try
            {
                var baseUrl = _uriService.GetBaseWebUri();
                var responseData = await _restAPIService.GetResponse<MedicalDocsRequirementResponse>(APIType.Client, "MedicalRecords/RequirementData/" + request.MedicalRecordsId, auth);

                var data = new DataSuratDto<DocsKematianRequest>();
                data.RequestData = request;
                data.ClinicData = responseData.ClinicData;
                data.PatientData = responseData.PatientData;
                data.OwnerData = responseData.OwnerData;
                data.MedicalData = responseData.MedicalData;
                data.VetName = responseData.VetName;
                data.PatientLatestStatistic = responseData.PatientLatestStatistic;

                string templatePath = PathHelper.GetTemplatePath(TemplateType.SuratKematian);
                string outputPath = PathHelper.GetGenerateOutputPath(TemplateType.SuratKematian, userId);
                string fileName = $"{data.MedicalData.Code}_SuratKematianGenerated.docx";
                string outputFile = Path.Combine(outputPath, fileName);
                // Check if the target folder exists
                if (!Directory.Exists(outputPath))
                {
                    // Create the folder if it does not exist
                    Directory.CreateDirectory(outputPath);
                }

                var url = $"{baseUrl}Generate/{userId}/SuratKematian/{fileName}";
                //populate data
                var patientAge = FormatUtil.GetAgeInfo(data.PatientData.DateOfBirth);
                string diedDateString = data.RequestData.DiedTime.ToString("dd"); // Day of the month
                string diedMonthString = data.RequestData.DiedTime.ToString("MMMM"); // Full month name
                string diedYearString = data.RequestData.DiedTime.ToString("yyyy"); // Year
                string diedTimeString = data.RequestData.DiedTime.ToString("HH:mm"); // Time in "HH:mm" format

                var clinicLogo = data.ClinicData.Logo;
                if (string.IsNullOrEmpty(clinicLogo) || !clinicLogo.Contains("http"))
                {
                    clinicLogo = "https://vethub.id/images/vethubsmall.png";
                }

                Dictionary<string, string> replacementValues = new Dictionary<string, string>
                {
                    { "{%clinic_logo1}", "{IMAGE_URL}" + clinicLogo },
                    { "{%clinic_logo2}", "{IMAGE_URL}" + clinicLogo },
                    { "{clinic_name}", data.ClinicData.Name },
                    { "{clinic_address}", data.ClinicData.Address },
                    { "{clinic_phone}", data.ClinicData.PhoneNumber },
                    { "{clinic_email}", data.ClinicData.Email },
                    { "{patient_name}", data.PatientData.Name },
                    { "{patient_species}", data.PatientData.Species },
                    { "{patient_color}", data.PatientData.Color },
                    { "{patient_age}", patientAge },
                    { "{patient_gender}", data.PatientData.Gender },
                    { "{patient_breed}", data.PatientData.Breed },
                    { "{patient_weight}", data.PatientLatestStatistic.Where(x => x.Type == "Weight").Select(x => x.Latest).FirstOrDefault() },
                    { "{medical_record_id}", data.MedicalData.Code },
                    { "{owner_name}", data.OwnerData.Name },
                    { "{owner_address}", data.OwnerData.Address },
                    { "{owner_phone}", data.OwnerData.PhoneNumber },
                    { "{died_date}", diedDateString },
                    { "{died_month}", diedMonthString },
                    { "{died_year}", diedYearString },
                    { "{died_time}", diedTimeString },
                    { "{died_reason}", data.RequestData.DiedReason },
                    { "{died_burried_info}", data.RequestData.DiedBuriedInfo },
                    { "{city}", data.ClinicData.City },
                    { "{year}", DateTime.Now.ToString("yyyy") },
                    { "{vet_name}", data.VetName }
                };
                //generate file
                GenerateDocX(templatePath, outputFile, replacementValues);

                //return new response
                DocGenerateResponse response = new DocGenerateResponse
                {
                    Filename = fileName,
                    Type = "SuratKematian",
                    Url = url
                };
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DocGenerateResponse> GenerateSuratPermintaanPulangAsync(string userId, DocsPermintaanPulangRequest request, string auth)
        {
            try
            {
                var baseUrl = _uriService.GetBaseWebUri();
                var responseData = await _restAPIService.GetResponse<MedicalDocsRequirementResponse>(APIType.Client, "MedicalRecords/RequirementData/" + request.MedicalRecordsId, auth);
                var responseStaff = await _restAPIService.GetResponse<Profile>(APIType.Client, "Profile/User/" + userId, auth);

                var data = new DataSuratDto<DocsPermintaanPulangRequest>();
                data.RequestData = request;
                data.ClinicData = responseData.ClinicData;
                data.PatientData = responseData.PatientData;
                data.OwnerData = responseData.OwnerData;
                data.MedicalData = responseData.MedicalData;
                data.VetName = responseData.VetName;
                data.PatientLatestStatistic = responseData.PatientLatestStatistic;
                data.StaffName = responseStaff.Name;

                string templatePath = PathHelper.GetTemplatePath(TemplateType.SuratTidakSetuju);
                string outputPath = PathHelper.GetGenerateOutputPath(TemplateType.SuratTidakSetuju, userId);
                string fileName = $"{data.MedicalData.Code}_SuratPulangTidakSetujuGenerated.docx";
                string outputFile = Path.Combine(outputPath, fileName);
                // Check if the target folder exists
                if (!Directory.Exists(outputPath))
                {
                    // Create the folder if it does not exist
                    Directory.CreateDirectory(outputPath);
                }

                var url = $"{baseUrl}Generate/{userId}/SuratPermintaanPulangAtauTidakSetuju/{fileName}";
                //populate data
                var patientAge = FormatUtil.GetAgeInfo(data.PatientData.DateOfBirth);
                string dateString = data.MedicalData.StartDate.ToString("dd MMMM yyyy"); // Year
                string duration = GetDuration(data.MedicalData.EndDate.Value, data.MedicalData.StartDate);
                var clinicLogo = data.ClinicData.Logo;
                if (string.IsNullOrEmpty(clinicLogo) || !clinicLogo.Contains("http"))
                {
                    clinicLogo = "https://vethub.id/images/vethubsmall.png";
                }

                Dictionary<string, string> replacementValues = new Dictionary<string, string>
                {
                    { "{%clinic_logo}", "{IMAGE_URL}" + clinicLogo },
                    { "{clinic_name}", data.ClinicData.Name },
                    { "{clinic_address}", data.ClinicData.Address },
                    { "{clinic_phone}", data.ClinicData.PhoneNumber },
                    { "{clinic_email}", data.ClinicData.Email },
                    { "{owner_name}", data.OwnerData.Name },
                    { "{owner_address}", data.OwnerData.Address },
                    { "{owner_phone}", data.OwnerData.PhoneNumber },
                    { "{owner_number_id}", data.RequestData.OwnerIdNumber },
                    { "{duration}", duration },
                    { "{patient_name}", data.PatientData.Name },
                    { "{patient_species}", data.PatientData.Species },
                    { "{patient_color}", data.PatientData.Color },
                    { "{patient_age}", patientAge },
                    { "{patient_gender}", data.PatientData.Gender },
                    { "{patient_breed}", data.PatientData.Breed },
                    { "{date}", dateString },
                    { "{city}", data.ClinicData.City },
                    { "{year}", DateTime.Now.ToString("yyyy") },
                    { "{staff_name}", data.StaffName },
                    { "{vet_name}", data.VetName }
                };
                //generate file
                GenerateDocX(templatePath, outputFile, replacementValues);

                //return new response
                DocGenerateResponse response = new DocGenerateResponse
                {
                    Filename = fileName,
                    Type = "SuratPermintaanPulangAtauTidakSetuju",
                    Url = url
                };
                return response;
            }
            catch
            {
                throw;
            }
        }


        //TODO: test table format
        public async Task<DocGenerateResponse> GenerateSuratTindakanAsync(string userId, DocsTindakanRequest request, string auth)
        {
            try
            {
                var baseUrl = _uriService.GetBaseWebUri();
                var responseStaff = await _restAPIService.GetResponse<Profile>(APIType.Client, "Profile/User/" + userId, auth);
                var responseData = await _restAPIService.GetResponse<MedicalDocsRequirementResponse>(APIType.Client, "MedicalRecords/RequirementData/" + request.MedicalRecordsId, auth);

                var data = new DataSuratDto<DocsTindakanRequest>();
                data.RequestData = request;
                data.ClinicData = responseData.ClinicData;
                data.PatientData = responseData.PatientData;
                data.OwnerData = responseData.OwnerData;
                data.MedicalData = responseData.MedicalData;
                data.VetName = responseData.VetName;
                data.PatientLatestStatistic = responseData.PatientLatestStatistic;
                data.StaffName = responseStaff.Name;

                string templatePath = PathHelper.GetTemplatePath(TemplateType.SuratPersetujuanTindakan);
                string outputPath = PathHelper.GetGenerateOutputPath(TemplateType.SuratPersetujuanTindakan, userId);
                string fileName = $"{data.MedicalData.Code}_SuratTindakanGenerated.docx";
                string outputFile = Path.Combine(outputPath, fileName);
                // Check if the target folder exists
                if (!Directory.Exists(outputPath))
                {
                    // Create the folder if it does not exist
                    Directory.CreateDirectory(outputPath);
                }

                var url = $"{baseUrl}Generate/{userId}/SuratPersetujuanTindakan/{fileName}";
                //populate data
                var patientAge = FormatUtil.GetAgeInfo(data.PatientData.DateOfBirth);
                string dateString = data.MedicalData.StartDate.ToString("dd MMMM yyyy"); // Year

                var clinicLogo = data.ClinicData.Logo;
                if (string.IsNullOrEmpty(clinicLogo) || !clinicLogo.Contains("http"))
                {
                    clinicLogo = "https://vethub.id/images/vethubsmall.png";
                }
                var patientTemp = data.PatientLatestStatistic.Where(x => x.Type == "Temperature").Select(x => x.Latest).FirstOrDefault();
                var patientWeight = data.PatientLatestStatistic.Where(x => x.Type == "Weight").Select(x => x.Latest).FirstOrDefault();

                Dictionary<string, string> replacementValues = new Dictionary<string, string>
                {
                    { "{%clinic_logo}", "{IMAGE_URL}" + clinicLogo },
                    { "{clinic_name}", data.ClinicData.Name },
                    { "{clinic_address}", data.ClinicData.Address },
                    { "{clinic_phone}", data.ClinicData.PhoneNumber },
                    { "{clinic_email}", data.ClinicData.Email },
                    { "{owner_name}", data.OwnerData.Name },
                    { "{owner_address}", data.OwnerData.Address },
                    { "{owner_id_number}", data.RequestData.OwnerIdNumber },
                    { "{vet_note}", data.RequestData.VetNote },
                    { "{total_amount}", "Rp " + data.RequestData.DepositAmount.ToString("##,0.00", new CultureInfo("id-ID")) },
                    { "{BULLET_action}", string.Join("\n", data.RequestData.MedicalAction) },
                    { "{BULLET_diagnose}", string.Join("\n", data.RequestData.MedicalDiagnose) },
                    { "{owner_phone}", data.OwnerData.PhoneNumber },
                    { "{date}", dateString },
                    { "{city}", data.ClinicData.City },
                    { "{year}", DateTime.Now.ToString("yyyy") },
                    { "{staff_name}", data.StaffName },
                };
                Dictionary<string, List<List<string>>> tableDataDictionary = new Dictionary<string, List<List<string>>>
                {
                    {
                        "{table_0}", new List<List<string>>
                        {
                            new List<string> { data.PatientData.Name, data.PatientData.Species, data.PatientData.Breed, $"{patientAge}, {data.PatientData.Color}", patientWeight, patientTemp },
                            // Add more rows as needed
                        }
                    }
                };
                //generate file
                GenerateDocXWithTables(templatePath, outputFile, replacementValues, tableDataDictionary);

                //return new response
                DocGenerateResponse response = new DocGenerateResponse
                {
                    Filename = fileName,
                    Type = "SuratPersetujuanTindakan",
                    Url = url
                };
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DocGenerateResponse> GenerateSuratRujukanAsync(string userId, DocsRujukanRequest request, string auth)
        {
            try
            {
                var baseUrl = _uriService.GetBaseWebUri();
                var responseStaff = await _restAPIService.GetResponse<Profile>(APIType.Client, "Profile/User/" + userId, auth);
                var responseData = await _restAPIService.GetResponse<MedicalDocsRequirementResponse>(APIType.Client, "MedicalRecords/RequirementData/" + request.MedicalRecordsId, auth);

                var data = new DataSuratDto<DocsRujukanRequest>();
                data.RequestData = request;
                data.ClinicData = responseData.ClinicData;
                data.PatientData = responseData.PatientData;
                data.OwnerData = responseData.OwnerData;
                data.MedicalData = responseData.MedicalData;
                data.VetName = responseData.VetName;
                data.MedicalDiagnoses = responseData.MedicalDiagnoses;
                data.PatientLatestStatistic = responseData.PatientLatestStatistic;
                data.StaffName = responseStaff.Name;

                string templatePath = PathHelper.GetTemplatePath(TemplateType.SuratRujukan);
                string outputPath = PathHelper.GetGenerateOutputPath(TemplateType.SuratRujukan, userId);
                string fileName = $"{data.MedicalData.Code}_SuratRujukanGenerated.docx";
                string outputFile = Path.Combine(outputPath, fileName);
                // Check if the target folder exists
                if (!Directory.Exists(outputPath))
                {
                    // Create the folder if it does not exist
                    Directory.CreateDirectory(outputPath);
                }

                var url = $"{baseUrl}Generate/{userId}/SuratRujukan/{fileName}";
                //populate data
                var patientAge = FormatUtil.GetAgeInfo(data.PatientData.DateOfBirth);

                var clinicLogo = data.ClinicData.Logo;
                if (string.IsNullOrEmpty(clinicLogo) || !clinicLogo.Contains("http"))
                {
                    clinicLogo = "https://vethub.id/images/vethubsmall.png";
                }
                string diagnoses = "";
                if (data.MedicalDiagnoses != null)
                {
                    var dd = data.MedicalDiagnoses.Select(x => x.Diagnose);
                    diagnoses = string.Join(", ", dd);
                }

                Dictionary<string, string> replacementValues = new Dictionary<string, string>
                {
                    { "{%clinic_logo}", "{IMAGE_URL}" + clinicLogo },
                    { "{clinic_name}", data.ClinicData.Name },
                    { "{clinic_address}", data.ClinicData.Address },
                    { "{clinic_phone}", data.ClinicData.PhoneNumber },
                    { "{clinic_email}", data.ClinicData.Email },
                    { "{clinic_refferal_name}", data.RequestData.ClinicRefferalName },
                    { "{owner_name}", data.OwnerData.Name },
                    { "{owner_address}", data.OwnerData.Address },
                    { "{owner_phone}", data.OwnerData.PhoneNumber },
                    { "{patient_name}", data.PatientData.Name },
                    { "{diagnose}", diagnoses },
                    { "{patient_species}", data.PatientData.Species },
                    { "{patient_age}", patientAge },
                    { "{patient_gender}", data.PatientData.Gender },
                    { "{patient_breed}", data.PatientData.Breed },
                    { "{patient_statistic_temperature}", data.PatientLatestStatistic.Where(x => x.Type == "Temperature").Select(x => x.Latest).FirstOrDefault() },
                    { "{patient_statistic_weight}", data.PatientLatestStatistic.Where(x => x.Type == "Weight").Select(x => x.Latest).FirstOrDefault() },
                    { "{BULLET_action}", string.Join("\n", data.RequestData.MedicalAction) },
                    { "{city}", data.ClinicData.City },
                    { "{year}", DateTime.Now.ToString("yyyy") },
                    { "{vet_name}", data.VetName }
                };
                //generate file
                GenerateDocX(templatePath, outputFile, replacementValues);

                //return new response
                DocGenerateResponse response = new DocGenerateResponse
                {
                    Filename = fileName,
                    Type = "SuratRujukan",
                    Url = url
                };
                return response;
            }
            catch
            {
                throw;
            }
        }

        private void GenerateDocX(string templatePath, string outputPath, Dictionary<string, string> replacementValues)
        {
            try
            {
                using (DocX templateDoc = DocX.Load(templatePath))
                {
                    foreach (var keyValue in replacementValues)
                    {
                        if (keyValue.Value != null)
                        {
                            if (keyValue.Value.StartsWith("{IMAGE_URL}"))
                            {
                                string imageUrl = keyValue.Value.Substring("{IMAGE_URL}".Length);
                                using (WebClient webClient = new WebClient())
                                {
                                    byte[] imageBytes = webClient.DownloadData(imageUrl);
                                    using (MemoryStream imageStream = new MemoryStream(imageBytes))
                                    {
                                        var picture = templateDoc.AddImage(imageStream);
                                        Xceed.Document.NET.Picture p = picture.CreatePicture();

                                        // Get the width and height of the original image
                                        float originalWidth = p.Width;
                                        float originalHeight = p.Height;

                                        // Calculate the new dimensions (you can customize these values)
                                        float newWidth = 75; // Set the desired new width
                                        float aspectRatio = originalWidth / originalHeight;
                                        float newHeight = newWidth / aspectRatio;

                                        // Resize the picture
                                        p.Width = newWidth;
                                        p.Height = newHeight;

                                        // Find the paragraph with the image placeholder and replace it
                                        foreach (var paragraph in templateDoc.Paragraphs)
                                        {
                                            if (paragraph.Text.Contains(keyValue.Key))
                                            {
                                                var run = paragraph.InsertPicture(p);
                                                paragraph.ReplaceText(keyValue.Key, "");
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (keyValue.Key.StartsWith("{BULLET"))
                            {
                                var values = keyValue.Value.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                // Find the paragraph with the bullet placeholder
                                var bulletParagraph = templateDoc.Paragraphs.FirstOrDefault(p => p.Text.Contains(keyValue.Key));
                                if (bulletParagraph != null)
                                {
                                    // Create a bulleted list with its first item
                                    var bulletedList = templateDoc.AddList(values[0], 0, ListItemType.Bulleted);

                                    // Add sub-items to the preceding ListItem
                                    for (int i = 1; i < values.Length; i++)
                                    {
                                        templateDoc.AddListItem(bulletedList, values[i], 0);
                                    }

                                    // Insert the list into the paragraph
                                    bulletParagraph.InsertListAfterSelf(bulletedList);

                                    templateDoc.ReplaceText(keyValue.Key, "");
                                }
                            }
                            else
                            {
                                templateDoc.ReplaceText(keyValue.Key, keyValue.Value);
                            }
                        }
                        else
                        {
                            templateDoc.ReplaceText(keyValue.Key, "");
                        }
                    }
                    templateDoc.SaveAs(outputPath);
                }
            }
            catch
            {
                throw;
            }
        }

        private void GenerateDocXWithTables(string templatePath, string outputPath, Dictionary<string, string> replacementValues, Dictionary<string, List<List<string>>> tableDataDictionary)
        {
            using (DocX templateDoc = DocX.Load(templatePath))
            {
                foreach (var keyValue in replacementValues)
                {
                    if (keyValue.Value.StartsWith("{IMAGE_URL}"))
                    {
                        string imageUrl = keyValue.Value.Substring("{IMAGE_URL}".Length);
                        using (WebClient webClient = new WebClient())
                        {
                            byte[] imageBytes = webClient.DownloadData(imageUrl);
                            using (MemoryStream imageStream = new MemoryStream(imageBytes))
                            {
                                var picture = templateDoc.AddImage(imageStream);
                                Xceed.Document.NET.Picture p = picture.CreatePicture();

                                // Get the width and height of the original image
                                float originalWidth = p.Width;
                                float originalHeight = p.Height;

                                // Calculate the new dimensions (you can customize these values)
                                float newWidth = 75; // Set the desired new width
                                float aspectRatio = originalWidth / originalHeight;
                                float newHeight = newWidth / aspectRatio;

                                // Resize the picture
                                p.Width = newWidth;
                                p.Height = newHeight;

                                // Find the paragraph with the image placeholder and replace it
                                foreach (var paragraph in templateDoc.Paragraphs)
                                {
                                    if (paragraph.Text.Contains(keyValue.Key))
                                    {
                                        var run = paragraph.InsertPicture(p);
                                        paragraph.ReplaceText(keyValue.Key, "");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (keyValue.Key.StartsWith("{BULLET"))
                    {
                        var values = keyValue.Value.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        // Find the paragraph with the bullet placeholder
                        var bulletParagraph = templateDoc.Paragraphs.FirstOrDefault(p => p.Text.Contains(keyValue.Key));
                        if (bulletParagraph != null)
                        {
                            // Create a bulleted list with its first item
                            var bulletedList = templateDoc.AddList(values[0], 0, ListItemType.Bulleted);

                            // Add sub-items to the preceding ListItem
                            for (int i = 1; i < values.Length; i++)
                            {
                                templateDoc.AddListItem(bulletedList, values[i], 0);
                            }

                            // Insert the list into the paragraph
                            bulletParagraph.InsertListAfterSelf(bulletedList);

                            templateDoc.ReplaceText(keyValue.Key, "");
                        }
                    }
                    else
                    {
                        templateDoc.ReplaceText(keyValue.Key, keyValue.Value);
                    }

                }

                // Add and populate tables
                foreach (var tableIdentifier in tableDataDictionary.Keys)
                {
                    if (tableIdentifier.StartsWith("{table"))
                    {
                        var index = ExtractNumber(tableIdentifier);
                        // Extract table index from identifier
                        int tableIndex = int.Parse(index);

                        Table table = templateDoc.Tables[tableIndex];
                        if (table != null)
                        {
                            List<List<string>> tableData = tableDataDictionary[tableIdentifier];
                            int rowIndex = 1;
                            foreach (var rowData in tableData)
                            {
                                if (rowIndex < table.Rows.Count)
                                {
                                    for (int colIndex = 0; colIndex < rowData.Count; colIndex++)
                                    {
                                        table.Rows[rowIndex].Cells[colIndex].Paragraphs[0].Append(rowData[colIndex]);
                                    }
                                }
                                else
                                {
                                    // Insert new row and populate cells
                                    Row newRow = table.InsertRow();
                                    for (int colIndex = 0; colIndex < rowData.Count; colIndex++)
                                    {
                                        newRow.Cells[colIndex].Paragraphs[0].Append(rowData[colIndex]);
                                    }
                                }

                                rowIndex++;
                            }
                        }
                    }
                }
                templateDoc.SaveAs(outputPath);
            }
        }
        private string ExtractNumber(string input)
        {
            // Using regular expression to match numbers within curly braces
            Match match = Regex.Match(input, @"\{table_(\d+)\}");

            if (match.Success)
            {
                // Extracting the captured number group
                string number = match.Groups[1].Value;
                return number;
            }
            else
            {
                return "Number not found.";
            }
        }
        private string GetDuration(DateTime endDate, DateTime startDate)
        {
            TimeSpan duration = endDate - startDate;
            string result = "";

            if (duration.TotalDays >= 1)
            {
                // If the duration is equal to or more than 1 day
                int days = (int)duration.TotalDays;
                result = $"{days} days";
            }
            else if (duration.TotalHours >= 1)
            {
                // If the duration is equal to or more than 1 hour
                int hours = (int)duration.TotalHours;
                result = $"{hours} hours";
            }
            else
            {
                // If the duration is in minutes
                int minutes = (int)duration.TotalMinutes;
                result = $"{minutes} minutes";
            }
            return result;
        }
    }
}
