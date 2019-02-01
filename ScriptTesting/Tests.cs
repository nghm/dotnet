using System;
using System.Linq.Expressions;

namespace ScriptTesting
{
    using System;

    internal class AsyncTestBuilderUnitTests
    {
        public static Action[] Tests = new[]
        {
            Something
                .Like(() => new AsyncBuilder(null))
                .Should
                .Throw<ArgumentNullException>()
        };
    }

    internal class AsyncBuilder
    {
        private object _o;

        public AsyncBuilder(object o)
        {
            this._o = o ?? throw new ArgumentNullException();
        }
    }
}

internal static class Something
{
    public static ISomething Like(Expression<Action> someExpression)
    {
        throw new NotImplementedException();
    }
    public static ISomething Like(Expression<Func<object>> someExpression)
    {
        throw new NotImplementedException();
    }
}

internal interface ISomething
{
    ITestable Should { get; set; }
}

internal interface ITestable
{
    Action Throw<T>();
}
