namespace Hypermedia.WebApi.Models
{
    using System;

    internal class BookDetailsModel
    {
        public string Title { get; set; }
        public Guid Id { get; set; }
        public string Description { get; internal set; }
    }
}