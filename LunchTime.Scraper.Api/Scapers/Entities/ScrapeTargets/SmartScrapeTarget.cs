using System.Collections.Generic;
using HtmlAgilityPack;
using LunchTime.Scraper.Api.LearnedScrapeTargets.Entities;

namespace LunchTime.Scraper.Api.Scapers.Entities.ScrapeTargets
{
    public class SmartScrapeTarget : IScrapeTarget
    {
        public HtmlDocument MatchedSegment { get; set; }

        public IEnumerable<HtmlNode> Scrape(HtmlDocument document)
        {
            throw new System.NotImplementedException();
        }
    }
}