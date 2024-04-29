namespace Core.Common.Dtos.NewsDtos
{
    public class NewsDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string ChannelTitle { get; set; } = string.Empty;
        public DateTime? PubDate { get; set; }
        public long ChannelId { get; set; }
    }
}