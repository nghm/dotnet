namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    using Builder;

    public interface IEntityBuilder : IBuilder<IEntity>
    {
        IEntityBuilder WithClasses(params string[] classes);
        IEntityBuilder WithProperties<TProp>(TProp properties);
        IEntityBuilder WithAction(IAction action);
        IEntityBuilder WithEntity(IEntity entity);
        IEntityBuilder WithLink(ILink link);
    }
}
