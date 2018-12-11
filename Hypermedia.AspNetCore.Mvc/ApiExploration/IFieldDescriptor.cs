namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    internal interface IFieldDescriptor
    {
        string Name { get; }
        object Value { get; }
    }
}
