namespace Hypermedia.AspNetCore.Siren.Actions
{
    using Actions.Fields;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal class ActionBuilder<TBody> : IActionBuilder<TBody>
    {
        private readonly Action _action;

        public ActionBuilder(Action action)
        {
            this._action = action;
        }

        public void WithOptions<TOption>(Expression<Func<TBody, TOption>> collectExpression, IEnumerable<KeyValuePair<string, TOption>> options)
        {
            if (!(collectExpression.Body is MemberExpression property))
            {
                return;
            }

            var name = property.Member.Name;

            if (!(this._action.Fields.Single(f => f.Name == name) is Field field))
            {
                return;
            }

            field.AddMetadata(KeyValuePair.Create("type", "option" as object));

            field.AddMetadata(KeyValuePair.Create("options", options.Select(kvp => new
            {
                name = kvp.Key,
                value = kvp.Value
            }) as object));
        }

        public void WithOptions<TOption>(Expression<Func<TBody, IEnumerable<TOption>>> collectExpression, IEnumerable<KeyValuePair<string, TOption>> options)
        {
            if (!(collectExpression.Body is MemberExpression property))
            {
                return;
            }

            var name = property.Member.Name;

            if (!(this._action.Fields.Single(f => f.Name == name) is Field field))
            {
                return;
            }

            field.AddMetadata(KeyValuePair.Create("type", "options" as object));

            field.AddMetadata(KeyValuePair.Create("options", options.Select(kvp => new
            {
                name = kvp.Key,
                value = kvp.Value
            }) as object));
        }
    }
}