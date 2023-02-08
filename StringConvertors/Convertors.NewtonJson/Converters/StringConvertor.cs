using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Convertors.Interfaces;
using Convertors.Models;
using Convertors.NewtonJson.Resolvers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Convertors.NewtonJson.Converters
{
    /// <summary>
    /// Using Newtonsoft.Json, Version=6.0
    /// </summary>
    /// <seealso cref="Tests.StringConvertors.IStringConvertor" />
    public class StringConvertor : IStringConvertor
    {
        private readonly JsonSerializerSettings m_JsonSerializerSettings;
        private readonly CustomSettings m_CustomSettings;

        public StringConvertor(IOptions<JsonSerializerSettings>? jsonSerializerSettings, IOptions<CustomSettings>? customSettings)
        {
            if (jsonSerializerSettings?.Value != null)
            {
                if (jsonSerializerSettings.Value.ContractResolver != null)
                {
                    throw new ArgumentException("JsonSerializerSettings ContractResolver must be Null.");
                }

                m_JsonSerializerSettings = jsonSerializerSettings.Value;
            }
            else
            {
                m_JsonSerializerSettings = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                };
            }

            m_CustomSettings = new CustomSettings()
            {
                IgnoreEmptyValues = true,
                IsCensoringEnabled = true,
            };

            m_CustomSettings.Update(customSettings?.Value);
        }

        public string ToJson(object value, bool censored = true)
        {
            if (value == null)
            {
                return string.Empty;
            }

            JsonSerializerSettings settings = this.GetSettings(censored, false);
            string result = JsonConvert.SerializeObject(value, settings);

            return result;
        }

        public T? FromJson<T>(string jsonString) where T : class
        {
            T? result = JsonConvert.DeserializeObject<T>(jsonString);
            return result;
        }

        public string ToQueryString(object value, bool censored = true)
        {
            JsonSerializerSettings settings = this.GetSettings(censored, false);
            var jObject = JObject.FromObject(value, JsonSerializer.Create(settings));
            string queryOutput = string.Join("&", jObject.Properties().Select(s => $"{s.Name}={HttpUtility.UrlEncode(s.Value.ToString())}"));

            return queryOutput;
        }

        public T? FromQueryString<T>(string queryString) where T : class
        {
            var dictionary = HttpUtility.ParseQueryString(queryString);
            var formDictionary = dictionary.AllKeys
                     .Where(p => dictionary[p] != "null")
                     .ToDictionary(p => p, p => dictionary[p]);

            JObject jObject = JObject.FromObject(formDictionary);

            T? result = jObject.ToObject<T>();

            return result;
        }

        public string ToXmlString(object value, string rootElementName, bool censored = true)
        {
            JsonSerializerSettings settings = this.GetSettings(censored, true);
            JObject jObject = JObject.FromObject(value, JsonSerializer.Create(settings));

            var converter = new XmlNodeConverter { DeserializeRootElementName = rootElementName };
            var settingsSerialization = new JsonSerializerSettings();
            settingsSerialization.Converters.Add(converter);

            var json = JsonSerializer.Create(settingsSerialization);
            XElement? xmlElement = jObject.ToObject<XElement>(json);
            XDocument xml = new XDocument(xmlElement);

            var writter = new StringWriter();
            SaveOptions options = (settings.Formatting == Formatting.None) ? SaveOptions.DisableFormatting : SaveOptions.None;
            xml.Save(writter, options);

            string xmlString = writter.ToString();
            return xmlString;
        }

        public T? FromXmlString<T>(string xmlString, bool omitRootObject) where T : class
        {
            var converter = new XmlNodeConverter { OmitRootObject = omitRootObject };
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(converter);

            XElement xml = XElement.Parse(xmlString);
            JObject jObject = JObject.FromObject(xml, JsonSerializer.Create(settings));

            T? result = jObject.ToObject<T>();
            return result;
        }

        #region Private

        private JsonSerializerSettings GetSettings(bool isCensoringEnabled, bool camelCase)
        {
            m_CustomSettings.Update(new CustomSettings()
            {
                IsCensoringEnabled = isCensoringEnabled,
                UseCamelCasePropertyNames = camelCase,
            });

            var settings = m_JsonSerializerSettings;

            settings.ContractResolver = new CustomSettingsResolver(m_CustomSettings);

            return settings;
        }

        #endregion
    }
}
