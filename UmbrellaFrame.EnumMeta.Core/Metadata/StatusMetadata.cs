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
            : this(value, message, type, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMetadata"/> class.
        /// </summary>
        public StatusMetadata(Enum value, string message, Enum type, string code, string externalCode)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Code = code;
            ExternalCode = externalCode;
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
        /// Gets the optional stable application code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the optional external integration code.
        /// </summary>
        public string ExternalCode { get; }

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

    /// <summary>
    /// Represents strongly typed status metadata resolved from an enum value.
    /// </summary>
    /// <typeparam name="TValue">The enum value type that owns the metadata.</typeparam>
    /// <typeparam name="TType">The enum type used as the status category.</typeparam>
    public sealed class StatusMetadata<TValue, TType>
        where TValue : struct, Enum
        where TType : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusMetadata{TValue, TType}"/> class.
        /// </summary>
        public StatusMetadata(TValue value, string message, TType type, string code, string externalCode)
        {
            Value = value;
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type;
            Code = code;
            ExternalCode = externalCode;
        }

        /// <summary>
        /// Gets the enum value that owns the metadata.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Gets the status message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the status category.
        /// </summary>
        public TType Type { get; }

        /// <summary>
        /// Gets the optional stable application code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the optional external integration code.
        /// </summary>
        public string ExternalCode { get; }
    }
}
