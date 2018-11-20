using LunchTime.Scraper.Api.Scapers.Entities;

namespace LunchTime.Main.Api.MenuConverters
{
    public interface IMenuConverterFactory
    {
        IMenuConverter GetConverter(ScrapeResultType scrapeResultType);
    }
}