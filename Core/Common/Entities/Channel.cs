using Microsoft.EntityFrameworkCore;

namespace Core.Common.Entities
{
    public class Channel : BaseEntity
    {
        [Comment("Заголовок канала новостей")]
        public string Title { get; set; } = string.Empty;

        [Comment("Описание канала новостей")]
        public string Description { get; set; } = string.Empty;

        [Comment("URL RSS канала")]
        public string RssLink { get; set; } = string.Empty;

        [Comment("URL канала")]
        public string Link { get; set; } = string.Empty;

        [Comment("Флаг фиксации. Позволяет защитить канал от удаления.")]
        public bool IsFixed { get; set; }

        public List<News> News { get; set; } = new List<News>();
    }
}
