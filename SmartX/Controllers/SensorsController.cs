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
    public class SensorsController : ControllerBase
    {
        private readonly SmartXStore _store;

        public SensorsController(SmartXStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IActionResult GetSensors()
        {
            return Ok(_store.Sensors);
        }

        [HttpPost]
        public IActionResult RegisterSensor([FromBody] SensorRegistration sensor)
        {
            var errors = SensorValidator.Validate(sensor);

            if (errors.Count > 0)
            {
                return BadRequest(new { errors });
            }

            var exists = _store.Sensors.Any(s =>
                s.DeviceId.Equals(
                    sensor.DeviceId,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (exists)
            {
                return Conflict(new
                {
                    message = "A sensor with this Device ID already exists."
                });
            }

            sensor.CreatedAt = DateTime.UtcNow;
            sensor.Status = "Online";

            _store.Sensors.Add(sensor);

            return Created(
                $"/api/sensors/{sensor.DeviceId}",
                sensor
            );
        }
    }
}

//Reference list
//ardalis (2026). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].
//tdykstra (2026). Upload files in ASP.NET CORE. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].