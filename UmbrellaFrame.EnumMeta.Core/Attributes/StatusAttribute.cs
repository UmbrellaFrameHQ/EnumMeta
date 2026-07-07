
using System;

namespace UmbrellaFrame.EnumMeta.Core
{
    /// <summary>
    /// Defines status metadata for an enum field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StatusAttribute : Attribute
    {
        /// <summary>
        /// Gets the message associated with the enum value.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the status type associated with the enum value.
        /// </summary>
        public Enum Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusAttribute"/> class.
        /// </summary>
        public StatusAttribute(string message, StatusType type)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusAttribute"/> class.
        /// </summary>
        [Obsolete("Use StatusAttribute(string, StatusType). C# attribute arguments cannot use System.Enum directly.")]
        public StatusAttribute(string message, Enum type)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
