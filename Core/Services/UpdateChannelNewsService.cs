using Core.Common.Entities;
using Core.Common.Exceptions;
using Core.Common.Interfaces.IServices;

namespace Core.Services
{
    public class UpdateChannelNewsService : IUpdateChannelNewsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConvertorXmlToRssObjectService _convertorXmlToRssObjectService;

        public UpdateChannelNewsService(IHttpClientFactory httpClientFactory, IConvertorXmlToRssObjectService convertorXmlToRssObjectService)
        {
            _httpClientFactory = httpClientFactory;
            _convertorXmlToRssObjectService = convertorXmlToRssObjectService;
        }

        public async Task<Channel> UpdateAsync(Channel channel)
        {
            // ----- отправляем запрос -----
            using HttpClient client = _httpClientFactory.CreateClient("HttpClient");

            try
            {
                HttpResponseMessage responseMessage = await client.GetAsync(channel.RssLink);

                // получаем rss xml строку
                string body = await responseMessage.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                // конвертируем xmlRss в Rss объект
                var rssObject = _convertorXmlToRssObjectService.Convert(body);

                channel.Link = rssObject.Channel.Link;

                channel.News = rssObject.News;

                foreach (var item in channel.News) item.ChannelId = channel.Id;
            }
            catch
            {
                throw new AppException(500, "Error updating news rss channel.");
            }

            return channel;
        }
    }
}
