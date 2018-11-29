namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Actions;
    using Links;
    using Parallel;

    public interface IEntityBuilder : IBuilder<IEntity>
    {
        IEntityBuilder WithClasses(params string[] classes);
        IEntityBuilder WithProperties<TProp>(TProp properties);
        IEntityBuilder WithAction(IAction action);
        IEntityBuilder WithEntity(IEntity entity);
        IEntityBuilder WithLink(ILink link);
    }
}
