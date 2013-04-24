using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Rebuild.Extensions
{
    public struct QueryStringBuilder
    {
        internal QueryStringBuilder(string query, bool hasParameters)
            : this()
        {
            Query = query;
            HasParameters = hasParameters;
        }

        public QueryStringBuilder AddParameter(string name, string value)
        {
            var separator = HasParameters ? '&' : '?';
            var query = Query + separator + HttpUtility.UrlEncode(name) + '=' + HttpUtility.UrlEncode(value);
            return new QueryStringBuilder(query, true);
        }

        public override string ToString()
        {
            return Query;
        }

        public Uri ToUri()
        {
            return new Uri(Query);
        }

        public static implicit operator string(QueryStringBuilder b)
        {
            return b.Query;
        }

        public static implicit operator Uri(QueryStringBuilder b)
        {
            return b.ToUri();
        }

        internal bool HasParameters { get; private set; }
        public string Query { get; private set; }
    }
}
