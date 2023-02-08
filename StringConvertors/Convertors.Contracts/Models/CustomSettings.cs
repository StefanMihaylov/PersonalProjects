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
            this.CensorAttibuteType = typeof(DisableAuditingAttribute);
            this.CensorStringMask = "*Censored*";
            this.IsCensoringEnabled = false;
            this.UseCamelCasePropertyNames = false;
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


        public void Update(CustomSettings? additionalSettings)
        {
            if (additionalSettings == null)
            {
                return;
            }

            if (additionalSettings.IgnorePropertyNameReplacement.HasValue)
            {
                this.IgnorePropertyNameReplacement = additionalSettings.IgnorePropertyNameReplacement.Value;
            }

            if (additionalSettings.IgnoreEmptyValues.HasValue)
            {
                this.IgnoreEmptyValues = additionalSettings.IgnoreEmptyValues.Value;
            }

            if (additionalSettings.IsCensoringEnabled.HasValue)
            {
                this.IsCensoringEnabled = additionalSettings.IsCensoringEnabled.Value;
            }

            if (additionalSettings.CensorAttibuteType != null)
            {
                this.CensorAttibuteType = additionalSettings.CensorAttibuteType;
            }

            if (!string.IsNullOrWhiteSpace(additionalSettings.CensorStringMask))
            {
                this.CensorStringMask = additionalSettings.CensorStringMask;
            }

            if (additionalSettings.UseCamelCasePropertyNames.HasValue)
            {
                this.UseCamelCasePropertyNames = additionalSettings.UseCamelCasePropertyNames.Value;
            }
        }
    }
}
