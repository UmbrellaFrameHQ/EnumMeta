using System;
using System.Collections.Generic;

namespace UmbrellaFrame.EnumMeta.Core
{
    /// <summary>
    /// Represents ordered informational metadata resolved from an enum value.
    /// </summary>
    public sealed class InfoMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InfoMetadata"/> class.
        /// </summary>
        public InfoMetadata(Enum value, string[] infos)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Infos = infos ?? Array.Empty<string>();
        }

        /// <summary>
        /// Gets the enum value that owns the metadata.
        /// </summary>
        public Enum Value { get; }

        /// <summary>
        /// Gets all info entries in attribute order.
        /// </summary>
        public IReadOnlyList<string> Infos { get; }

        /// <summary>
        /// Gets the display name when available.
        /// </summary>
        public string Name => GetOrDefault(InfoType.Name);

        /// <summary>
        /// Gets the description when available.
        /// </summary>
        public string Description => GetOrDefault(InfoType.Description);

        /// <summary>
        /// Gets a single info entry, or an empty string when that entry is missing.
        /// </summary>
        public string GetOrDefault(InfoType infoType)
        {
            var index = (int)infoType;
            return index >= 0 && index < Infos.Count ? Infos[index] : string.Empty;
        }
    }
}
