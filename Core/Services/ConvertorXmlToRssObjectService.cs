using Core.Common.Dtos.RssDto;
using Core.Common.Entities;
using Core.Common.Interfaces.IServices;
using System.Xml;

namespace Core.Services
{
    public class ConvertorXmlToRssObjectService : IConvertorXmlToRssObjectService
    {
        public RssDto Convert(string xmlRss)
        {
            var returnRss = new RssDto();

            // загружаем xml
            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xmlRss);

            // получаем корневой элемент
            XmlNode root = xmlDocument["rss"]!;

            if (root == null) return returnRss;

            // получаем елемент channel
            XmlNode channel = root["channel"]!;

            if (root == null) return returnRss;

            // заполняем данными объект rss
            if (channel["title"] != null) returnRss.Channel.Title = channel["title"]!.InnerText;
            if (channel["description"] != null) returnRss.Channel.Description = channel["description"]!.InnerText;
            if (channel["link"] != null) returnRss.Channel.Link = channel["link"]!.InnerText;

            // создаем список новостей и заполняем его
            var listNews = new List<News>();

            foreach (XmlElement element in channel.ChildNodes)
            {
                if (element.Name != "item") continue;

                News news = new News();

                if (element["title"] != null) news.Title = element["title"]!.InnerText;
                if (element["description"] != null) news.Description = element["description"]!.InnerText;
                if (element["link"] != null) news.Link = element["link"]!.InnerText;
                if (element["pubDate"] != null) news.PubDate = DateTime.Parse(element["pubDate"]!.InnerText).ToUniversalTime();

                listNews.Add(news);
            }

            // передаем список в возвращаемый объект
            returnRss.News = listNews;

            return returnRss;
        }
    }
}
