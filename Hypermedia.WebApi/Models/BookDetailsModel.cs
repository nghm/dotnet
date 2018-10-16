namespace Hypermedia.WebApi.Models
{
    internal class BookDetailsModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string Description { get; internal set; }
    }
}