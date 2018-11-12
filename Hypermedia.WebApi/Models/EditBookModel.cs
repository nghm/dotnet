namespace Books.WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Services;

    public class EditBookModel
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required, MaxLength(255)]
        public string Description { get; set; }
        //public BookStatus Status { get; set; }
        public bool IsFree { get; set; }

        public string[] Tags { get; set; }
    }
}