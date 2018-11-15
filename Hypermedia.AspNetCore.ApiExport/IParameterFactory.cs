namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.AspNetCore.Mvc.Abstractions;

    internal interface IParameterFactory
    {
        ParameterDefinition Make(ParameterDescriptor parameterDescriptor);
    }

    internal class ParameterFactory : IParameterFactory
    {
        private readonly ITypeFactory _typeFactory;

        public ParameterFactory(ITypeFactory typeFactory)
        {
            this._typeFactory = typeFactory;
        }

        public ParameterDefinition Make(ParameterDescriptor parameterDescriptor)
        {
            return new ParameterDefinition()
            {
                Name = parameterDescriptor.Name,
                Binding = parameterDescriptor.BindingInfo.BindingSource.Id,
                Type = this._typeFactory.Make(parameterDescriptor.ParameterType)
            };
        }
    }
}