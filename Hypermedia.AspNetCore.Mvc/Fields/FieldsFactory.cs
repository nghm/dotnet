namespace Hypermedia.AspNetCore.Mvc.Fields
{
    using ApiExploration;
    using System;
    using System.Linq.Expressions;

    internal class FieldFactory : IFieldFactory
    {
        private readonly string _name;
        private readonly Func<object, object> _getValue;

        private FieldFactory(Type body, string name)
        {
            this._getValue = MakeValueAccessor(body, name);
            this._name = name;
        }

        public IFieldDescriptor Make(object body)
        {
            return new FieldDescriptor(this._name, () => this._getValue(body));
        }

        private static Func<object, object> MakeValueAccessor(Type body, string name)
        {
            var x = Expression.Parameter(body, "x");
            var property = Expression.Property(x, name);

            return Expression.Lambda<Func<object, object>>(property, x).Compile();
        }
    }
}