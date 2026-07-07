
using System;
using UmbrellaFrame.EnumMeta.Core.Internal;

namespace UmbrellaFrame.EnumMeta.Core
{
    /// <summary>
    /// Provides extension methods for reading informational metadata from enum values.
    /// </summary>
    public static class EnumInfoExtensions
    {
        /// <summary>
        /// Gets all required <see cref="InfoAttribute"/> entries for an enum value.
        /// </summary>
        public static string[] GetEnumInfos(this Enum status)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            if (status.TryGetEnumInfos(out var infos))
            {
                return infos;
            }

            throw new InvalidOperationException(
                $"Enum value '{status}' does not define an {nameof(InfoAttribute)}.");
        }

        /// <summary>
        /// Gets a required info entry by <see cref="InfoType"/>.
        /// </summary>
        public static string GetEnumInfo(this Enum status, InfoType infoType)
        {
            var infos = status.GetEnumInfos();
            var index = (int)infoType;

            if (index < 0 || index >= infos.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(infoType),
                    infoType,
                    $"Enum value '{status}' does not have info metadata at index {index}.");
            }

            return infos[index];
        }

        /// <summary>
        /// Gets an info entry by <see cref="InfoType"/>, or a fallback value when it is unavailable.
        /// </summary>
        public static string GetEnumInfoOrDefault(this Enum status, InfoType infoType, string defaultValue = "")
        {
            if (!status.TryGetEnumInfos(out var infos))
            {
                return defaultValue;
            }

            var index = (int)infoType;
            return index >= 0 && index < infos.Length ? infos[index] : defaultValue;
        }

        /// <summary>
        /// Gets a typed metadata object for an enum value with info metadata.
        /// </summary>
        public static InfoMetadata GetInfoMetadata(this Enum status)
        {
            return new InfoMetadata(status, status.GetEnumInfos());
        }

        /// <summary>
        /// Attempts to get a typed metadata object for an enum value with info metadata.
        /// </summary>
        public static bool TryGetInfoMetadata(this Enum status, out InfoMetadata metadata)
        {
            metadata = null;

            if (!status.TryGetEnumInfos(out var infos))
            {
                return false;
            }

            metadata = new InfoMetadata(status, infos);
            return true;
        }

        /// <summary>
        /// Attempts to get all <see cref="InfoAttribute"/> entries for an enum value.
        /// </summary>
        public static bool TryGetEnumInfos(this Enum status, out string[] infos)
        {
            infos = Array.Empty<string>();

            if (status == null)
            {
                return false;
            }

            var attribute = EnumMetadataCache.Get(status).InfoAttribute;
            if (attribute == null)
            {
                return false;
            }

            infos = attribute.Infos;
            return true;
        }
    }
}
