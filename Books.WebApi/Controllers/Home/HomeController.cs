namespace Books.WebApi.Controllers.Home
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [HasMenu]
        public IActionResult Get()
        {
            return Ok(new HomeResource());
        }
    }
}
