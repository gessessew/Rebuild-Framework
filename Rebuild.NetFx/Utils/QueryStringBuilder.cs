using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Rebuild.Utils
{
    public struct QueryStringBuilder
    {
        internal QueryStringBuilder(string query, bool hasParameters)
            : this()
        {
            Query = query;
            HasParameters = hasParameters;
        }

        public QueryStringBuilder AddParameter(string name, bool value)
        {
            return AddParameter(name, value.ToString());
        }

        public QueryStringBuilder AddParameter(string name, byte value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, decimal value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, double value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, float value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, Guid value, string format = "G")
        {
            return AddParameter(name, value.ToString(format, CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, int value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, long value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, sbyte value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, string value)
        {
            var separator = HasParameters ? '&' : '?';
            var query = Query + separator + HttpUtility.UrlEncode(name) + '=' + HttpUtility.UrlEncode(value);
            return new QueryStringBuilder(query, true);
        }

        public QueryStringBuilder AddParameter(string name, short value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, ushort value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, uint value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, ulong value)
        {
            return AddParameter(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public QueryStringBuilder AddParameter(string name, Uri value)
        {
            return AddParameter(name, value.ToString());
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
