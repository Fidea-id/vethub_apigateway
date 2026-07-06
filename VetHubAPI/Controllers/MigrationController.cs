using Application.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VetHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Superadmin")]
    public class MigrationController : Controller
    {
        private readonly IMedicalRecordNoteMigrationService _migrationService;

        public MigrationController(IMedicalRecordNoteMigrationService migrationService)
        {
            _migrationService = migrationService;
        }

        [HttpPost("MigrateNotes")]
        public async Task<IActionResult> MigrateNotes([FromQuery] string dbName, [FromQuery] int batchCount = 50)
        {
            try
            {
                // Validation:
                // 1. dbName is required
                if (string.IsNullOrWhiteSpace(dbName))
                {
                    return BadRequest(new { Message = "dbName is required." });
                }

                // 2. Reject batchCount < 1 or > 500
                if (batchCount < 1 || batchCount > 500)
                {
                    return BadRequest(new { Message = "batchCount must be between 1 and 500." });
                }

                // 3. Reject system databases
                var systemDbs = new[] { "vethubmaster", "mysql", "information_schema", "performance_schema", "sys" };
                if (systemDbs.Contains(dbName.ToLower()))
                {
                    return BadRequest(new { Message = "Execution against system databases is not allowed." });
                }

                // 4. Validate that dbName exists in tenant registry
                string? authToken = HttpContext.Request.Headers["Authorization"];
                var isValidTenant = await _migrationService.ValidateTenantDatabaseAsync(dbName, authToken);
                if (!isValidTenant)
                {
                    return BadRequest(new { Message = $"Database '{dbName}' is not registered as a tenant." });
                }

                // Audit information:
                var userId = User.FindFirstValue("Id") ?? User.FindFirst("Id")?.Value ?? "0";
                var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirst(ClaimTypes.Email)?.Value ?? "unknown";
                var triggeredAt = DateTime.UtcNow;

                // Log audit information
                _migrationService.LogAuditInfo(userId, userEmail, dbName, batchCount, triggeredAt);

                // Enqueue Hangfire background job
                var jobId = _migrationService.EnqueueMigrationJob(dbName, batchCount);

                return Ok(new
                {
                    JobId = jobId,
                    DatabaseName = dbName,
                    BatchCount = batchCount,
                    Message = "Migration job enqueued successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
