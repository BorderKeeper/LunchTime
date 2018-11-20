using LunchTime.Core.Api.Common;
using LunchTime.Scraper.Api.LearnedScrapeTargets.Entities;
using LunchTime.Scraper.Api.LearnedScrapeTargets.Queries;

namespace LunchTime.Scraper.Api.LearnedScrapeTargets
{
    public interface IScrapeTargetQueryHandler : IQueryHandler<ScrapeTargetQuery, IScrapeTarget>
    {       
    }
}