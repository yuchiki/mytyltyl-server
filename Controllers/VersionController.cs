using Microsoft.AspNetCore.Mvc;

namespace mytyltyl_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;

        public VersionController(ILogger<VersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetVersion")]
        public VersionResponse Get()
        {
            Console.WriteLine("request");
            return new VersionResponse { MajorVersion = 0, MinorVersion = 0, PatchVersion = 0, StartedAt = DateTime.Today };
        }
    }
}
