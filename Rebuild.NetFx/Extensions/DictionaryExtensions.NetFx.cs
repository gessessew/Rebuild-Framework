using System.Collections.Generic;
using System.Collections.Specialized;

namespace Rebuild.Extensions
{
    public static partial class DictionaryExtensions
    {
        public static NameValueCollection ToNameValueCollection(this IDictionary<string, string> dic, IEqualityComparer<string> comparer = null)
        {
            if (comparer == null)
            {
                comparer = (dic as Dictionary<string, string>).IfNotNull(d => d.Comparer) ?? EqualityComparer<string>.Default;
            }

            var nvc = new NameValueCollection(comparer.ToUnTypedEqualityComparer());

            foreach (var kv in dic)
            {
                nvc[kv.Key] = kv.Value;
            }

            return nvc;
        }
    }
}
