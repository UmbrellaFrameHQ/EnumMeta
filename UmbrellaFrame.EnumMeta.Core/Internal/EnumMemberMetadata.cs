namespace UmbrellaFrame.EnumMeta.Core.Internal
{
    internal sealed class EnumMemberMetadata
    {
        public static readonly EnumMemberMetadata Empty = new EnumMemberMetadata(null, null);

        public EnumMemberMetadata(StatusAttribute statusAttribute, InfoAttribute infoAttribute)
        {
            StatusAttribute = statusAttribute;
            InfoAttribute = infoAttribute;
        }

        public StatusAttribute StatusAttribute { get; }

        public InfoAttribute InfoAttribute { get; }
    }
}
