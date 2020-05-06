using System;

namespace Convertors.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSettings"/> class.
        /// </summary>
        public CustomSettings()
        {
            this.IgnorePropertyNameReplacement = false;
            this.IgnoreEmptyValues = false;

            this.IsCensoringEnabled = false;
            this.CensorAttibuteType = typeof(DisableAuditingAttribute);
            this.CensorStringMask = "*Censored*";

            this.UseCamelCasePropertyNames = false;
        }

        public CustomSettings(CustomSettings other)
            : this()
        {
            if (other.IgnorePropertyNameReplacement.HasValue)
            {
                this.IgnorePropertyNameReplacement = other.IgnorePropertyNameReplacement.Value;
            }

            if (other.IgnoreEmptyValues.HasValue)
            {
                this.IgnoreEmptyValues = other.IgnoreEmptyValues.Value;
            }

            if (other.IsCensoringEnabled.HasValue)
            {
                this.IsCensoringEnabled = other.IsCensoringEnabled.Value;
            }

            if (other.CensorAttibuteType != null)
            {
                this.CensorAttibuteType = other.CensorAttibuteType;
            }

            if (!string.IsNullOrWhiteSpace(other.CensorStringMask))
            {
                this.CensorStringMask = other.CensorStringMask;
            }

            if (other.UseCamelCasePropertyNames.HasValue)
            {
                this.UseCamelCasePropertyNames = other.UseCamelCasePropertyNames.Value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [ignore property name replacement]. Default: False
        /// </summary>
        public bool? IgnorePropertyNameReplacement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ignore empty values]. Default: False
        /// </summary>
        public bool? IgnoreEmptyValues { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property censoring is enabled. Default: False
        /// </summary>
        public bool? IsCensoringEnabled { get; set; }

        /// <summary>
        /// Gets or sets the type of the censor attribute. Default: DisableAuditingAttribute
        /// </summary>
        public Type CensorAttibuteType { get; set; }

        /// <summary>
        /// Gets or sets the censor string mask. Default: '*Censored*'
        /// </summary>
        public string CensorStringMask { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use camel case property names]. Default: False
        /// </summary>
        public bool? UseCamelCasePropertyNames { get; set; }
    }
}
