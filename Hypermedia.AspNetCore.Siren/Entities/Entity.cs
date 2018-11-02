using Hypermedia.AspNetCore.Siren.Actions;
using Hypermedia.AspNetCore.Siren.Links;
using Hypermedia.AspNetCore.Siren.Util;
using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.Entities
{
    class Entity : IEntity
    {
        public Entity(string[] classes, string href)
        {
            Href = href;
            Classes = classes;
        }

        public Entity(
            string[] classes, 
            IEntity[] entities = null, 
            ILink[] links = null, 
            IDictionary<string, object> properties = null, 
            IAction[] actions = null
        )
        {
            Classes = classes;
            Entities = entities;
            Links = links;
            Properties = properties
                .AsEnumerable()
                .ToDictionary(kvp => kvp.Key.ToCamelCase(), kvp => kvp.Value);
            Actions = actions;
        }

        public string[] Classes { get; set; }
        public IEntity[] Entities { get; set; }
        public ILink[] Links { get; set; }
        public IDictionary<string, object> Properties { get; set; }
        public IAction[] Actions { get; internal set; }

        public string Href { get; set; }

    }
}
