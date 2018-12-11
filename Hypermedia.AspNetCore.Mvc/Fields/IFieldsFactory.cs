namespace Hypermedia.AspNetCore.Mvc.Fields
{
    using ApiExploration;

    internal interface IFieldFactory
    {
        IFieldDescriptor Make(object arguments);
    }
}