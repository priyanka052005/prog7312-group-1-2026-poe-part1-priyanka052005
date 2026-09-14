//The Smart-X API controllers were developed with assistance from Microsoft learn to structure API endpoints,
//HTTP requests and responses, dependency injection and communication between the frontend and backend.
//Microsoft Learn documentation on ASP.NET Core controllers was used as technical guidance.
//The sensor attachment functionality was additionally informed by Microsoft documentation on multipart
//file uploads

using Microsoft.AspNetCore.Mvc;
using SmartX.Models;
using SmartX.Services;

namespace SmartX.Controllers
{
    [ApiController]
    [Route("api/sensors")]
    public class SensorAttachmentsController : ControllerBase
    {
        private readonly SmartXStore _store;
        private readonly IWebHostEnvironment _environment;
        private readonly FileEncryptionService _encryptionService;

        public SensorAttachmentsController(
            SmartXStore store,
            IWebHostEnvironment environment,
            FileEncryptionService encryptionService)
        {
            _store = store;
            _environment = environment;
            _encryptionService = encryptionService;
        }

        [HttpPost("{deviceId}/upload")]
        public async Task<IActionResult> UploadFile(
            string deviceId)
        {
            var sensor =
                _store.Sensors.FirstOrDefault(s =>
                    s.DeviceId.Equals(
                        deviceId,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            if (sensor == null)
            {
                return NotFound(new
                {
                    message = "Sensor not found."
                });
            }

            if (!Request.HasFormContentType)
            {
                return BadRequest(new
                {
                    message =
                        "Use multipart/form-data."
                });
            }

            var form =
                await Request.ReadFormAsync();

            var file =
                form.Files.FirstOrDefault();

            if (file == null ||
                file.Length == 0)
            {
                return BadRequest(new
                {
                    message =
                        "Choose a file to upload."
                });
            }

            const long maxBytes =
                5 * 1024 * 1024;

            if (file.Length > maxBytes)
            {
                return BadRequest(new
                {
                    message =
                        "File must be 5 MB or smaller."
                });
            }

            var allowedExtensions =
                new[]
                {
                    ".txt",
                    ".log",
                    ".json",
                    ".jpg",
                    ".jpeg",
                    ".png",
                    ".pdf"
                };

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLowerInvariant();

            if (!allowedExtensions.Contains(
                extension))
            {
                return BadRequest(new
                {
                    message =
                        "Allowed: .txt, .log, .json, .jpg, .jpeg, .png, .pdf"
                });
            }

            var uploadsFolder =
                Path.Combine(
                    _environment.ContentRootPath,
                    "Uploads",
                    deviceId
                );

            Directory.CreateDirectory(
                uploadsFolder
            );

            var safeFileName =
                $"{Guid.NewGuid():N}.enc";

            var fullPath =
                Path.Combine(
                    uploadsFolder,
                    safeFileName
                );

            await using var inputStream =
                file.OpenReadStream();

            await _encryptionService
                .EncryptFileAsync(
                    inputStream,
                    fullPath
                );

            var attachment =
                new SensorAttachment
                {
                    DeviceId =
                        deviceId,

                    OriginalFileName =
                        Path.GetFileName(
                            file.FileName
                        ),

                    StoredFileName =
                        safeFileName,

                    UploadedAt =
                        DateTime.UtcNow,

                    SizeBytes =
                        file.Length
                };

            _store.Attachments.Add(
                attachment
            );

            return Ok(new
            {
                message =
                    "File uploaded and encrypted successfully.",

                attachment
            });
        }
    }
}

//Reference list
//ardalis (2026). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].
//tdykstra (2026). Upload files in ASP.NET CORE. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].