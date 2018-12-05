namespace Books.WebApi.Controllers.Feedback
{
    using System.ComponentModel.DataAnnotations;

    public class FeedbackModel
    {
        [Required, RegularExpression("[a-zA-Z0-9 ]*")]
        public string Message { get; set; }
    }
}