﻿using Hypermedia.AspNetCore.Core;

namespace Hypermedia.AspNetCore.AsyncStepBuilder
{
    using System.Threading.Tasks;

    public interface IAsyncBuildStep<in TBuilder, in TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        Task BuildAsync(TBuilder builder);
    }
}