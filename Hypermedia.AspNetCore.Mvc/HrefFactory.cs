namespace Hypermedia.AspNetCore.Mvc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using static System.String;

    internal class HrefFactory : IHrefFactory
    {
        private readonly string _template;
        private readonly (string Name, int Index)[] _query;
        private readonly (string Name, int Index)[] _route;

        public HrefFactory(string template, (string Name, int Index)[] query, (string Name, int Index)[] route)
        {
            this._template = template;
            this._query = query;
            this._route = route;
        }

        public string Make(object[] arguments)
        {
            var result = this._template;
            var args = arguments.Select(a => a.ToString()).ToArray();

            result = this.ApplyRoute(result, args);
            result = this.ApplyQuery(result, args);

            return result;
        }

        private string ApplyQuery(string result, IReadOnlyList<string> arguments)
        {
            var query = HttpUtility.ParseQueryString(Empty);

            foreach (var (name, index) in this._query)
            {
                query.Add(name, arguments[index]);
            }

            return result + query;
        }

        private string ApplyRoute(string result, IReadOnlyList<string> arguments)
        {
            foreach (var (name, index) in this._route)
            {
                result = Regex.Replace(result, $"\\{{ *{name}[:a-zA-Z0-9 ]*\\}}", arguments[index]);
            }

            return result;
        }
    }
}
