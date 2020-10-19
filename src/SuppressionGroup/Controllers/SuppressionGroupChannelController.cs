using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SuppressionGroup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuppressionGroupChannelController : ControllerBase
    {
        private readonly ILogger<SuppressionGroupChannelController> _logger;
        private readonly SuppressionGroupChannelRepository _repository;

        public SuppressionGroupChannelController(ILogger<SuppressionGroupChannelController> logger, SuppressionGroupChannelRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
                return Ok(await _repository.GetModel(id));
        }

        [HttpGet]
        [Route("/all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAll());
        }
    }
}
