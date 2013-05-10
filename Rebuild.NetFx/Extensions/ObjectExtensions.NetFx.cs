using Newtonsoft.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Rebuild.Extensions
{
    public static partial class ObjectExtensions
    {
        public static string SerializeToJsonString(this object obj, Newtonsoft.Json.Formatting formating = Newtonsoft.Json.Formatting.None, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(obj, formating, settings);
        }

        public static string SerializeToXmlString<T>(this T obj, XmlWriterSettings settings = null)
        {
            var sb = new StringBuilder();
            using (var w = XmlWriter.Create(sb, settings))
            {
                new XmlSerializer(typeof(T)).Serialize(w, obj);
            }
            return sb.ToString();
        }
    }
}
