namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;
    using System.Reflection;

    internal class CollectedMethodCall
    {
        public CollectedMethodCall(object[] arguments, MethodInfo method, Type target)
        {
            this.Arguments = arguments;
            this.Method = method;
            this.Target = target;
        }

        public object[] Arguments { get; }
        public MethodInfo Method { get; }
        public Type Target { get; }
    }
}