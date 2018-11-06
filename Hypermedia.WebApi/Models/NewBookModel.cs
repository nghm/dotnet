namespace Books.WebApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class NewBookModel
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        [MaxLength(255)]
        [RegularExpression("Test")]
        public string Description { get; set; }
    }
}