namespace Hypermedia.AspNetCore.Siren.Entities
{
    using Actions;
    using Hypermedia.AspNetCore.Core;
    using Links;

    public interface IEntityBuilder : IBuilder<IEntity>
    {
        IEntityBuilder WithClasses(params string[] classes);
        IEntityBuilder WithProperties<TProp>(TProp properties);
        IEntityBuilder WithAction(IAction action);
        IEntityBuilder WithEntity(IEntity entity);
        IEntityBuilder WithLink(ILink link);
    }
}
