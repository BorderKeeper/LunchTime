using System.Collections.Generic;
using HtmlAgilityPack;
using LunchTime.Scraper.Api.LearnedScrapeTargets.Entities;

namespace LunchTime.Scraper.Api.Scapers.Entities.ScrapeTargets
{
    public class NaiveScrapeTarget : IScrapeTarget
    {
        private readonly string _xPath;

        public NaiveScrapeTarget(string xPath)
        {
            _xPath = xPath;
        }

        public IEnumerable<HtmlNode> Scrape(HtmlDocument document)
        {
            return document.DocumentNode.SelectNodes(_xPath);
        }
    }
}