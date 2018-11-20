using System.Threading.Tasks;
using LunchTime.Core.Api.Common;
using LunchTime.Scraper.Api.Scapers.Entities;
using LunchTime.Scraper.Api.Scapers.Queries;

namespace LunchTime.Scraper.Api.Scapers
{
    public interface IScrapePageContentQueryHandler : IQueryHandler<ScrapePageContentQuery, Task<ScrapeResult>>
    {      
    }
}