using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace UmbrellaFrame.EnumMeta.Core.Internal
{
    internal static class EnumMetadataCache
    {
        private static readonly ConcurrentDictionary<Enum, EnumMemberMetadata> Cache =
            new ConcurrentDictionary<Enum, EnumMemberMetadata>();

        public static EnumMemberMetadata Get(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Cache.GetOrAdd(value, Create);
        }

        private static EnumMemberMetadata Create(Enum value)
        {
            var members = value.GetType().GetMember(value.ToString());
            if (members.Length == 0)
            {
                return EnumMemberMetadata.Empty;
            }

            var member = members[0];
            return new EnumMemberMetadata(
                GetAttribute<StatusAttribute>(member),
                GetAttribute<InfoAttribute>(member));
        }

        private static T GetAttribute<T>(MemberInfo member)
            where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(member, typeof(T), false);
        }
    }
}
