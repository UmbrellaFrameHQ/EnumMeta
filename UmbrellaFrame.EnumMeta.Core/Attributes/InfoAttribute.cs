
using System;

namespace UmbrellaFrame.EnumMeta.Core
{
    /// <summary>
    /// Defines ordered informational metadata for an enum field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class InfoAttribute : Attribute
    {
        /// <summary>
        /// Gets the info entries associated with the enum value.
        /// </summary>
        public string[] Infos { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoAttribute"/> class.
        /// </summary>
        public InfoAttribute(params string[] infos)
        {
            Infos = infos ?? Array.Empty<string>();
        }
    }
}
