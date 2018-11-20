using System.Threading.Tasks;
using HtmlAgilityPack;
using LunchTime.Core.Api;

namespace LunchTime.Scraper.Scrapers
{
    public interface IWebsiteContentQueryHandler : ISingletonQueryHandler<WebsiteContentQuery, Task<HtmlDocument>>
    {
    }
}