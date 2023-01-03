using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yuchiki.MytyltylApi;

namespace mytyltyl_server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public partial class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;

        public VersionController(ILogger<VersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetVersion")]
        public VersionResponse Get()
        {
            _logger.LogInformation("[{DT}] GET /Version", DateTime.UtcNow.ToLongTimeString());

            var version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

            return new VersionResponse
            {
                MajorVersion = version.ProductMajorPart,
                MinorVersion = version.ProductMinorPart,
                PatchVersion = version.ProductBuildPart,
                Suffix = version.ProductVersion == null ? "" : SuffixRegex().Match(version.ProductVersion).Value,
                StartedAt = Process.GetCurrentProcess().StartTime
            };

        }

        [GeneratedRegex("(?<=-)(.*)")]
        private static partial Regex SuffixRegex();
    }
}

