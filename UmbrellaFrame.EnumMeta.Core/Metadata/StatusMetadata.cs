using System;

namespace UmbrellaFrame.EnumMeta.Core
{
    /// <summary>
    /// Represents status metadata resolved from an enum value.
    /// </summary>
    public sealed class StatusMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMetadata"/> class.
        /// </summary>
        public StatusMetadata(Enum value, string message, Enum type)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Gets the enum value that owns the metadata.
        /// </summary>
        public Enum Value { get; }

        /// <summary>
        /// Gets the status message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the status category.
        /// </summary>
        public Enum Type { get; }

        /// <summary>
        /// Gets a value indicating whether the status type is <see cref="StatusType.Success"/>.
        /// </summary>
        public bool IsSuccess => Type.Equals(StatusType.Success);

        /// <summary>
        /// Gets a value indicating whether the status type is <see cref="StatusType.Warning"/>.
        /// </summary>
        public bool IsWarning => Type.Equals(StatusType.Warning);

        /// <summary>
        /// Gets a value indicating whether the status type is <see cref="StatusType.Error"/>.
        /// </summary>
        public bool IsError => Type.Equals(StatusType.Error);
    }
}
