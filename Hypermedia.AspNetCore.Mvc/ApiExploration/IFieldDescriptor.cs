namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    using System;

    internal interface IFieldDescriptor
    {
        string Name { get; }
        Type Type { get; }

        bool IsRequired { get; }
    }
}
