namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    public interface IField
    {
        string Name { get; }

        object Value { get; }
    }
}