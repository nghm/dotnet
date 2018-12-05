namespace Books.WebApi.Controllers.Feedback
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(FeedbackModel feedbackModel)
        {
            return Ok();
        }
    }
}
