namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.EntityFrameworkCore.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;

    internal interface ITypeFactory
    {
        TypeDescriptor Make(Type parameterType);
        IEnumerable<TypeDescriptor> GetAllTypes();
        string GetReferences();
    }

    internal class TypeFactory : ITypeFactory
    {
        private List<string> _dependencyList = new List<string>();
        private IDictionary<Type, TypeDescriptor> _cache = new Dictionary<Type, TypeDescriptor>();
        private readonly IPropertyFactory _propertyFactory;

        public TypeFactory()
        {
            this._propertyFactory = new PropertyFactory(this);
        }

        public TypeDescriptor Make(Type parameterType)
        {
            if (this._cache.ContainsKey(parameterType))
            {
                return this._cache[parameterType];
            }

            var typeDescriptor = new TypeDescriptor
            {
                Name = parameterType.Name,
                Type = Type(parameterType),
                Properties = parameterType
                    .GetProperties()
                    .Where(p => p.SetMethod != null && p.SetMethod.IsPublic)
                    .Select(p => this._propertyFactory.Make(p))
                    .ToArray()
            };

            this._cache[parameterType] = typeDescriptor;

            return typeDescriptor;
        }

        private string Type(Type parameterType)
        {

            if (!parameterType.IsClass)
            {
                this._dependencyList.Add(parameterType.Namespace);

                return "Primitive";
            }

            if (parameterType.IsPrimitive)
            {
                return "Primitive";
            }

            return "Model";
        }

        public IEnumerable<TypeDescriptor> GetAllTypes()
        {
            return this._cache
                .Values
                .Where(t => t.Type == "Model");
        }

        public string GetReferences()
        {
            return this._dependencyList
                .Distinct()
                .Select(n => $"       using {n};")
                .Join("\n");
        }
    }
}