namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using ApiExploration;
    using Fields;

    internal interface IFieldsFactories
    {
        IFieldsFactory Get(IApiActionDescriptor descriptor);
    }
}