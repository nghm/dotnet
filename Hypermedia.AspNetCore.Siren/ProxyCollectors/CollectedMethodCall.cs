using System;
using System.Reflection;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    internal class CollectedMethodCall
    {
        public object[] Arguments { get; internal set; }
        public MethodInfo Method { get; internal set; }
        public Type Target { get; internal set; }
    }
}