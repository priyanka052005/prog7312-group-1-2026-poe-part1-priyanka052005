//The Smart-X API controllers were developed with assistance from Microsoft learn to structure API endpoints,
//HTTP requests and responses, dependency injection and communication between the frontend and backend.
//Microsoft Learn documentation on ASP.NET Core controllers was used as technical guidance.
//The sensor attachment functionality was additionally informed by Microsoft documentation on multipart
//file uploads

using Microsoft.AspNetCore.Mvc;
using SmartX.Models;
using SmartX.Services;
using SmartX.Validators;

namespace SmartX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly SmartXStore _store;
        private readonly TelemetryService _service;
        private readonly MockTelemetryGenerator _generator;

        public TelemetryController(
            SmartXStore store,
            TelemetryService service,
            MockTelemetryGenerator generator)
        {
            _store = store;
            _service = service;
            _generator = generator;
        }

        [HttpGet]
        public IActionResult GetTelemetry()
        {
            var telemetry = _store.Telemetry
                .OrderByDescending(t => t.Timestamp);

            return Ok(telemetry);
        }

        [HttpPost]
        public IActionResult SendTelemetry([FromBody] TelemetryInput input)
        {
            var validationErrors =
                TelemetryValidator.Validate(input);

            if (validationErrors.Count > 0)
            {
                return BadRequest(new
                {
                    errors = validationErrors
                });
            }

            var sensorExists =
                _store.Sensors.Any(s =>
                    s.DeviceId.Equals(
                        input.DeviceId,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            if (!sensorExists)
            {
                return BadRequest(new
                {
                    message =
                        "Register the sensor before sending telemetry."
                });
            }

            try
            {
                var reading =
                    _service.ProcessInput(input);

                return Ok(reading);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("mock/{count:int}")]
        public IActionResult GenerateMockTelemetry(int count)
        {
            if (count < 1 || count > 5000)
            {
                return BadRequest(new
                {
                    message =
                        "Count must be between 1 and 5000."
                });
            }

            var generated =
                _generator.GenerateBatch(count);

            return Ok(new
            {
                message =
                    $"{generated.Count} mock telemetry records generated.",

                recordsGenerated =
                    generated.Count
            });
        }

        [HttpPost("demo-batch")]
        public IActionResult ProcessDemoBatch()
        {
            double[][] batches =
            [
                [22.4, 22.8, 23.1],
                [23.5, 24.2],
                [24.8, 25.1, 25.4, 25.0]
            ];

            var result =
                _service.ProcessHistoricalBatches(batches);

            return Ok(new
            {
                message =
                    "Jagged telemetry batches processed into List<double>.",

                batches,

                flattenedReadings =
                    result
            });
        }
    }
}

//Reference list
//ardalis (2026). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].
//tdykstra (2026). Upload files in ASP.NET CORE. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].