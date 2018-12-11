namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using ApiExploration;

    internal interface IFieldsFactory
    {
        IFieldDescriptor[] Make(object[] arguments);
    }
}