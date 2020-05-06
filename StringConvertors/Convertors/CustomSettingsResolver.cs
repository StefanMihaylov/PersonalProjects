using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using Convertors.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Convertors
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DefaultContractResolver" />
    public class CustomSettingsResolver : DefaultContractResolver
    {
        private readonly CustomSettings m_Settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSettingsResolver"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public CustomSettingsResolver(CustomSettings settings)
        {
            m_Settings = settings;
        }

        /// <summary>
        /// Creates a <see cref="T:JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
        /// <returns>
        /// A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            UseOriginalPropertyName(property);
            ConvertToCamelCasePropertyName(property);
            CensorPropertyValue(member, property);

            property.ShouldSerialize = instance =>
            {
                bool noEmptyValue = RemoveEmptyProperies(member, instance);
                return noEmptyValue;
            };

            return property;
        }

        #region Private 

        private void UseOriginalPropertyName(JsonProperty property)
        {
            if (m_Settings.IgnorePropertyNameReplacement == true)
            {
                property.PropertyName = property.UnderlyingName;
            }
        }

        private void CensorPropertyValue(MemberInfo member, JsonProperty property)
        {
            if (m_Settings.IsCensoringEnabled == false ||
               (m_Settings.IsCensoringEnabled == true && (m_Settings.CensorAttibuteType == null ||
                    string.IsNullOrWhiteSpace(m_Settings.CensorStringMask))))
            {
                return;
            }

            var reflectionPropety = member as PropertyInfo;
            if (reflectionPropety != null)
            {
                if (reflectionPropety.PropertyType.IsAssignableFrom(typeof(string)))
                {
                    var isSensitiveData = Attribute.IsDefined((PropertyInfo)member, m_Settings.CensorAttibuteType);
                    if (isSensitiveData)
                    {
                        Attribute attribute = reflectionPropety.GetCustomAttribute(m_Settings.CensorAttibuteType);

                        Pattern pattern = Pattern.Censored;
                        if (attribute is DisableAuditingAttribute customAttribute)
                        {
                            pattern = customAttribute.Pattern;
                        }

                        property.ValueProvider = new StringValueProvider(pattern, m_Settings.CensorStringMask, reflectionPropety);
                    }
                }
            }
        }

        private bool RemoveEmptyProperies(MemberInfo member, object instance)
        {
            if (m_Settings.IgnoreEmptyValues == false)
            {
                return true;
            }

            var reflectionPropety = member as PropertyInfo;
            if (reflectionPropety != null)
            {
                Type propertyType = reflectionPropety.PropertyType;
                if (propertyType.IsAssignableFrom(typeof(string)))
                {
                    string value = reflectionPropety.GetValue(instance) as string;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return false;
                    }
                }
                else if (propertyType.GetInterface(nameof(IEnumerable)) != null)
                {
                    object objectValue = reflectionPropety.GetValue(instance);
                    if (objectValue != null)
                    {
                        IEnumerable collection = objectValue as IEnumerable;
                        if (collection != null)
                        {
                            bool hasElements = collection.GetEnumerator().MoveNext();
                            return hasElements;
                        }
                    }
                }
                else
                {
                    // more validations
                }
            }

            return true;
        }

        private void ConvertToCamelCasePropertyName(JsonProperty property)
        {
            if (m_Settings.UseCamelCasePropertyNames == true)
            {
                property.PropertyName = ToCamelCase(property.PropertyName);
            }
        }

        private string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (!char.IsUpper(input[0]))
            {
                return input;
            }

            char[] chars = input.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                    break;

                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }

        #endregion

        private class StringValueProvider : IValueProvider
        {
            private const char CENSORING_CHAR = '*';

            private readonly string _censoredValue;
            private readonly Pattern _pattern;
            private PropertyInfo _propertyInfo;

            public StringValueProvider(Pattern pattern, string censoredValue, PropertyInfo propertyInfo)
            {
                _censoredValue = censoredValue;
                _pattern = pattern;
                _propertyInfo = propertyInfo;
            }

            public void SetValue(object target, object value)
            {
                throw new NotSupportedException();
            }

            public object GetValue(object target)
            {
                string value = _propertyInfo.GetValue(target) as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    return target;
                }

                switch (_pattern)
                {
                    case Pattern.All:
                        return new string(CENSORING_CHAR, value.Length);
                    case Pattern.CreditCard:
                        if (value.Length < 10)
                        {
                            return new string(CENSORING_CHAR, value.Length);
                        }

                        var builder = new StringBuilder();

                        builder.Append(value.Substring(0, 6));
                        builder.Append(new string(CENSORING_CHAR, value.Length - 10));
                        builder.Append(value.Substring(value.Length - 4, 4));

                        return builder.ToString();
                    case Pattern.Censored:
                    default:
                        return _censoredValue;
                }
            }
        }
    }
}
