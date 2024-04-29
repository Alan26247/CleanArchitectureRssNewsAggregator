using Microsoft.EntityFrameworkCore;

namespace Core.Common.Entities
{
    public class BaseEntity
    {
        [Comment("Уникальный идентификатор")]
        public long Id { get; set; }

        [Comment("Дата создания")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Comment("Дата обновления")]
        public DateTime? UpdatedAt { get; set; }
    }
}
