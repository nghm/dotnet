using System;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    [Serializable]
    internal class CallCollectionFinishedException : Exception
    {
        public CollectedMethodCall Call { get; }

        public CallCollectionFinishedException(CollectedMethodCall call)
        {
            this.Call = call;
        }
    }
}