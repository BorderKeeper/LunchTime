using System;
using LunchTime.Scraper.Api.LearnedScrapeTargets.Entities;
using LunchTime.Scraper.Api.Scapers.Entities;

namespace LunchTime.Scraper.Api.Scapers.Queries
{
    public class ScrapePageContentQuery
    {
        public Uri WebsiteUri { get; set; }

        public IScrapeTarget Target { get; set; }

        public ScrapeResultType ScrapeResultType { get; set; }
    }
}