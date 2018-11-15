namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Reflection;

    internal interface IPropertyFactory
    {
        PropertyDefinition Make(PropertyInfo propertyInfo);
    }

    internal class PropertyFactory : IPropertyFactory
    {
        private readonly ITypeFactory _factory;

        public PropertyFactory(ITypeFactory factory)
        {
            this._factory = factory;
        }

        public PropertyDefinition Make(PropertyInfo propertyInfo)
        {
            return new PropertyDefinition()
            {
                Name = propertyInfo.Name,
                Type = this._factory.Make(propertyInfo.PropertyType)
            };
        }
    }
}