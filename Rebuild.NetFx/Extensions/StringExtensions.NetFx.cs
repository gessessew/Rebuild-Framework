using Newtonsoft.Json;
using Rebuild.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Rebuild.Extensions
{
    public static partial class StringExtensions
    {
        public static T DeserializeJson<T>(this string s, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<T>(s, settings);
        }

        public static T DeserializeXml<T>(this string s, XmlReaderSettings settings = null)
        {
            using (var sr = new StringReader(s))
            using (var r = XmlReader.Create(sr, settings))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(r);
            }
        }

        public static NameValueCollection ParseQueryString(this string query)
        {
            return HttpUtility.ParseQueryString(query);
        }

        public static string RemoveDiacritics(this string s)
        {
            var b = new StringBuilder(s.Normalize(NormalizationForm.FormD));

            for (var i = b.Length - 1; i > -1; i--)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(b[i]) == UnicodeCategory.NonSpacingMark)
                {
                    b.Remove(i, 1);
                }
            }

            return b.ToString();
        }

        public static QueryStringBuilder ToQueryString(this string query)
        {
            if (query.EndsWith("/"))
                query = query.Left(query.Length - 1);

            return new QueryStringBuilder(query, query.IndexOf('?') > -1);
        }
    }
}
