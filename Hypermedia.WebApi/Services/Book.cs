namespace Hypermedia.WebApi.Services
{
    using System;

    public class Book : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public BookStatus Status { get; set; }
        public bool IsFree { get; set; }
    }
}
