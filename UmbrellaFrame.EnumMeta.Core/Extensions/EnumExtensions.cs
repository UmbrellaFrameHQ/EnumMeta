
using System;
using System.Globalization;
using System.Resources;
using UmbrellaFrame.EnumMeta.Core.Internal;

namespace UmbrellaFrame.EnumMeta.Core
{
    /// <summary>
    /// Provides extension methods for reading status metadata from enum values.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the required <see cref="StatusAttribute"/> for an enum value.
        /// </summary>
        public static StatusAttribute GetEnumStatus(this Enum status)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            if (status.TryGetEnumStatus(out var attribute))
            {
                return attribute;
            }

            throw new InvalidOperationException(
                $"Enum value '{status}' does not define a {nameof(StatusAttribute)}.");
        }

        /// <summary>
        /// Attempts to get the <see cref="StatusAttribute"/> for an enum value.
        /// </summary>
        public static bool TryGetEnumStatus(this Enum status, out StatusAttribute attribute)
        {
            attribute = null;

            if (status == null)
            {
                return false;
            }

            attribute = EnumMetadataCache.Get(status).StatusAttribute;
            return attribute != null;
        }

        /// <summary>
        /// Gets a typed metadata object for an enum value with status metadata.
        /// </summary>
        public static StatusMetadata GetStatusMetadata(this Enum status)
        {
            var attribute = status.GetEnumStatus();
            return new StatusMetadata(status, attribute.Message, attribute.Type, attribute.Code, attribute.ExternalCode);
        }

        /// <summary>
        /// Attempts to get a typed metadata object for an enum value with status metadata.
        /// </summary>
        public static bool TryGetStatusMetadata(this Enum status, out StatusMetadata metadata)
        {
            metadata = null;

            if (!status.TryGetEnumStatus(out var attribute))
            {
                return false;
            }

            metadata = new StatusMetadata(status, attribute.Message, attribute.Type, attribute.Code, attribute.ExternalCode);
            return true;
        }

        /// <summary>
        /// Gets a strongly typed metadata object for an enum value with status metadata.
        /// </summary>
        public static StatusMetadata<TValue, TType> GetStatusMetadata<TValue, TType>(this TValue status)
            where TValue : struct, Enum
            where TType : struct, Enum
        {
            var enumValue = (Enum)(object)status;
            var attribute = enumValue.GetEnumStatus();

            if (!(attribute.Type is TType type))
            {
                throw new InvalidOperationException(
                    $"Status metadata type '{attribute.Type.GetType().FullName}' cannot be used as '{typeof(TType).FullName}'.");
            }

            return new StatusMetadata<TValue, TType>(
                status,
                attribute.Message,
                type,
                attribute.Code,
                attribute.ExternalCode);
        }

        /// <summary>
        /// Attempts to get a strongly typed metadata object for an enum value with status metadata.
        /// </summary>
        public static bool TryGetStatusMetadata<TValue, TType>(this TValue status, out StatusMetadata<TValue, TType> metadata)
            where TValue : struct, Enum
            where TType : struct, Enum
        {
            metadata = null;

            var enumValue = (Enum)(object)status;
            if (!enumValue.TryGetEnumStatus(out var attribute) || !(attribute.Type is TType type))
            {
                return false;
            }

            metadata = new StatusMetadata<TValue, TType>(
                status,
                attribute.Message,
                type,
                attribute.Code,
                attribute.ExternalCode);
            return true;
        }

        /// <summary>
        /// Resolves a message for an enum value using a caller-provided message resolver.
        /// </summary>
        public static string GetResolvedMessage(this Enum status, Func<Enum, string> messageResolver, string fallbackMessage = "Message not available.")
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            if (messageResolver == null)
            {
                throw new ArgumentNullException(nameof(messageResolver));
            }

            return messageResolver(status) ?? fallbackMessage;
        }

        /// <summary>
        /// Resolves a localized message for an enum value using a caller-provided message resolver.
        /// </summary>
        public static string GetLocalizedMessage(this Enum status, Func<Enum, CultureInfo, string> messageResolver, string language = "en", string fallbackMessage = "Message not available.")
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            if (messageResolver == null)
            {
                throw new ArgumentNullException(nameof(messageResolver));
            }

            var culture = new CultureInfo(language);
            return messageResolver(status, culture) ?? fallbackMessage;
        }

        /// <summary>
        /// Reads a localized resource string from a caller-provided resource manager using the enum member name as the resource key.
        /// </summary>
        public static string GetLocalizedMessage(this Enum status, ResourceManager resourceManager, string language = "en")
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            if (resourceManager == null)
            {
                throw new ArgumentNullException(nameof(resourceManager));
            }

            var culture = new CultureInfo(language);
            return resourceManager.GetString(status.ToString(), culture) ?? "Message not available.";
        }
    }
}
