namespace Books.WebApi.Controllers
{
    using Hypermedia.AspNetCore.Siren;

    internal class HasMenuAttribute : PartialResourceAttribute
    {
        public HasMenuAttribute() : base(new MenuPartialResource())
        {
        }
    }
}