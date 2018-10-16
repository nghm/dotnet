namespace Hypermedia.WebApi.Services
{
    public class BooksService : MockCrudService<Book>
    {
        public BooksService() : base(
            id => new Book
            {
                Id = id,
                Title = $"Book #no {id}",
                Description = $"Book description no #{id}"
            }, 
            book => book.Id, 
            (id, entity) => entity.Id = id)
        {
        }
    }
}
