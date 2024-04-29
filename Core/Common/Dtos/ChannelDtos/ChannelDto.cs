namespace Core.Common.Dtos.ChannelDtos
{
    public class ChannelDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RssLink { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public bool IsFixed { get; set; }
    }
}
