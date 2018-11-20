using System.Windows.Forms;
using LunchTime.Core.Api.Common;
using LunchTime.Scraper.Api.Scapers.Queries;

namespace LunchTime.Scraper.Api.Scapers
{
    public interface IWebsiteContentQueryHandler : IQueryHandler<WebsiteContentQuery, HtmlDocument>
    {
    }
}