namespace Hypermedia.AspNetCore.Siren.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IActionBuilder<TBody>
    {
        void WithOptions<TOption>(
            Expression<Func<TBody, IEnumerable<TOption>>> collectExpression,
            IEnumerable<KeyValuePair<string, TOption>> options);
        void WithOptions<TOption>(
            Expression<Func<TBody, TOption>> collectExpression,
            IEnumerable<KeyValuePair<string, TOption>> options);
    }
}