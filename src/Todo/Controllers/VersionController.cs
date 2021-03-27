using Microsoft.AspNetCore.Mvc;

namespace Todo.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public string Get(ApiVersion version) => $"Version:{version}";
    }
}
