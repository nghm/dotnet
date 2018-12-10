namespace Hypermedia.AspNetCore.Mvc.AccessValidation
{
    using ApiExploration;

    public interface IAccessValidators
    {
        IAccessValidator Get(IApiActionDescriptor descriptor);
    }
}