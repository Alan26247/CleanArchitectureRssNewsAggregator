using Core.Common.Dtos.RssDto;

namespace Core.Common.Interfaces.IServices
{
    public interface IConvertorXmlToRssObjectService
    {
        /// <summary>
        /// Конвертирует строку xml RSS в объект RssDto
        /// </summary>
        /// <param name="xmlRss">строка xml rss</param>
        /// <returns>Возвращает объект RssDto с новостями</returns>
        public RssDto Convert(string xmlRss);
    }
}
