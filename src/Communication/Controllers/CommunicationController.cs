using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Communication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationController : ControllerBase
    {
        private readonly ILogger<CommunicationController> _logger;

        public CommunicationController(ILogger<CommunicationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetByIdAsync()
        {
            return NoContent();
        }
    }
}
