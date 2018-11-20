using System;

namespace LunchTime.Scraper.Api.Scapers.Entities
{
    public class ScrapeResult
    {
        public ScrapeResultType ResultyType { get; set; }

        public string Result { get; set; }

        public bool Equals(ScrapeResult result)
        {
            return ResultyType == result.ResultyType && Result.Equals(result.Result, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}