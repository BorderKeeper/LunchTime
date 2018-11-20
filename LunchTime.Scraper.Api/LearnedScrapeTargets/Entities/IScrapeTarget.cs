using System.Collections.Generic;
using HtmlAgilityPack;

namespace LunchTime.Scraper.Api.LearnedScrapeTargets.Entities
{
    public interface IScrapeTarget
    {
        IEnumerable<HtmlNode> Scrape(HtmlDocument document);
    }
}