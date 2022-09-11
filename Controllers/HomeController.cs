using Microsoft.AspNetCore.Mvc;

namespace SeoAlpha.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
