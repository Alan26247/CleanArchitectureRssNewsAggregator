using Microsoft.EntityFrameworkCore;

namespace Core.Common.Entities
{
    public class News : BaseEntity
    {
        [Comment("Заголовок")]
        public string Title { get; set; } = string.Empty;

        [Comment("Описание")]
        public string Description { get; set; } = string.Empty;

        [Comment("Ссылка на новость")]
        public string Link { get; set; } = string.Empty;

        [Comment("Дата публикации")]
        public DateTime PubDate { get; set; } = DateTime.UtcNow;

        [Comment("ID канала")]
        public long ChannelId { get; set; }

        public Channel Channel { get; set; }
    }
}
