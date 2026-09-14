//The Smart-X API controllers were developed with assistance from Microsoft learn to structure API endpoints,
//HTTP requests and responses, dependency injection and communication between the frontend and backend.
//Microsoft Learn documentation on ASP.NET Core controllers was used as technical guidance.
//The sensor attachment functionality was additionally informed by Microsoft documentation on multipart
//file uploads

using Microsoft.AspNetCore.Mvc;
using SmartX.Models;
using SmartX.Validators;

namespace SmartX.Controllers
{
    [ApiController]
    [Route("api/demo")]
    public class DemoController : ControllerBase
    {
        [HttpGet("operator-overload")]
        public IActionResult OperatorOverload()
        {
            var meter1 =
                new PowerReading("Meter-1", 420);

            var meter2 =
                new PowerReading("Meter-2", 380);

            var total =
                meter1 + meter2;

            return Ok(new
            {
                meter1,
                meter2,
                total
            });
        }

        [HttpGet("recursive-validation")]
        public IActionResult RecursiveValidation()
        {
            var hierarchy =
                DeploymentNode.CreateDemoHierarchy();

            var result =
                DeploymentValidator.ValidatePath(
                    hierarchy
                );

            return Ok(new
            {
                valid = result,
                hierarchy
            });
        }
    }
}

//Reference list
//ardalis (2026). Handle requests with controllers in ASP.NET core MVC. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/actions?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].
//tdykstra (2026). Upload files in ASP.NET CORE. [online] MicrosoftLearn. Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-10.0 [Accessed 14 Sept. 2026].