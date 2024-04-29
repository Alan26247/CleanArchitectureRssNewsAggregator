using Core.Common.Entities;

namespace Core.Common.Dtos.RssDto
{
    public class RssDto
    {
        public Entities.Channel Channel { get; set; } = new Channel();
        public List<News> News { get; set; } = new List<News>();
    }
}
