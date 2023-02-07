using System;

namespace Convertors.Models
{
    /// <summary>
    /// Used to disable auditing for a single method or
    /// all methods of a class or interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditingAttribute : Attribute
    {
        public Pattern Pattern { get; set; }

        public DisableAuditingAttribute()
        {
            this.Pattern = Pattern.All;
        }
    }
}
