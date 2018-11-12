namespace Hypermedia.AspNetCore.Siren.Entities
{
    using Actions;
    using Links;
    using Util;
    using System.Collections.Generic;
    using System.Linq;

    internal class Entity : IEntity
    {
        public Entity(string[] classes, string href)
        {
            this.Href = href;
            this.Classes = classes;
        }

        public Entity(
            string[] classes, 
            IEntity[] entities = null, 
            ILink[] links = null, 
            IDictionary<string, object> properties = null, 
            IAction[] actions = null
        )
        {
            this.Classes = classes;
            this.Entities = entities;
            this.Links = links;
            this.Properties = properties
                .AsEnumerable()
                .GroupBy(kvp => kvp.Key.ToCamelCase())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Last().Value);
            this.Actions = actions;
        }

        public string[] Classes { get; set; }
        public IEntity[] Entities { get; set; }
        public ILink[] Links { get; set; }
        public IDictionary<string, object> Properties { get; set; }
        public IAction[] Actions { get; internal set; }

        public string Href { get; set; }

    }
}
