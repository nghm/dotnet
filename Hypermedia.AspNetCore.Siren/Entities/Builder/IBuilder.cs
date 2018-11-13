namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    internal interface IBuilder<out T> : IBuilder
    {
        T Build();
    }

    internal interface IBuilder
    {
        object BuildAnonymous();
    }
}
