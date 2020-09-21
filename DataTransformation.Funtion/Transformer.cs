using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataTransformation.Funtion
{
    /// <summary>
    /// Transformer.
    /// </summary>
    public static class Transformer
    {
        /// <summary>
        /// Transforms the XML to JSON.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns>JSON as string.</returns>
        public static string TransformXmlToJson<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var stringReader = new System.IO.StringReader(xml);
            using var xmlReader = XmlReader.Create(stringReader);
            var obj = (T)serializer.Deserialize(xmlReader);

            // Add root property
            var jsonObjectAttribute = typeof(T).GetCustomAttribute(typeof(JsonObjectAttribute)) as JsonObjectAttribute;
            var jsonValue = JValue.FromObject(obj);
            var jsonObject =  new JObject(new JProperty(jsonObjectAttribute.Title, jsonValue));

            var settings = new JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented };
            return JsonConvert.SerializeObject(jsonObject, settings);
        }

        /// <summary>
        /// Transforms the JSON to XML.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="json">The json.</param>
        /// <returns>XML as string.</returns>
        public static string TransformJsonToXml<T>(string json)
        {
            // Remove root property
            var jsonObjectAttribute = typeof(T).GetCustomAttribute(typeof(JsonObjectAttribute)) as JsonObjectAttribute;
            JObject root = JObject.Parse(json);
            var data = root[jsonObjectAttribute.Title];

            var obj = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));

            var serializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter);
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("","");
            serializer.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
            return stringWriter.ToString();
        }
    }
}
