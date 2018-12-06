namespace Hypermedia.AspNetCore.Siren.Entities
{
    using Builders.Abstractions;

    internal class Entity : IEntity
    {
        public Entity(IClasses classes, string href)
        {
            this.Href = href;
            this.Classes = classes;
        }


        public Entity(
            IClasses classes,
            IEntity[] entities = null,
            ILink[] links = null,
            IProperties properties = null,
            IAction[] actions = null
        )
        {
            this.Classes = classes;
            this.Entities = entities;
            this.Links = links;
            this.Properties = properties;
            this.Actions = actions;
        }
        public IClasses Classes { get; set; }
        public IEntity[] Entities { get; set; }
        public ILink[] Links { get; set; }
        public IProperties Properties { get; set; }
        public IAction[] Actions { get; internal set; }

        public string Href { get; set; }

    }
}
