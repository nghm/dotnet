namespace Hypermedia.AspNetCore.Mvc.Fields
{
    using ApiExploration;
    using System;

    internal class FieldDescriptor : IFieldDescriptor
    {
        private readonly Func<object> _valueAccessor;

        public FieldDescriptor(string name, Func<object> valueAccessor)
        {
            this._valueAccessor = valueAccessor;
            this.Name = name;
        }

        public string Name { get; }
        public object Value => this._valueAccessor();
    }
}