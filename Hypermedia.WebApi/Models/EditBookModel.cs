using System.ComponentModel.DataAnnotations;
using Hypermedia.WebApi.Services;

namespace Hypermedia.WebApi.Models
{
    public class EditBookModel
    {
        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required, MaxLength(255)]
        public string Description { get; set; }
        public BookStatus Status { get; set; }
        public bool IsFree { get; set; }
    }
}