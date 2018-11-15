namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.Extensions.DependencyInjection;

    internal interface IProjectBuilder
    {
        ServiceProvider Build();
    }
}