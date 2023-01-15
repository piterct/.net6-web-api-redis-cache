using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Redis.Cache.API.Commands;

namespace Redis.Cache.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LikeController : Controller
    {
        private readonly ILogger<LikeController> _logger;

        public LikeController(ILogger<LikeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreateLikeCommand command)
        {
            try
            {
                return Ok(command);
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
