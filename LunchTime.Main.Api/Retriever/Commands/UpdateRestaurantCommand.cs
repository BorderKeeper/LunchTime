using LunchTime.Scraper.Api.Scapers.Entities;

namespace LunchTime.Main.Api.Retriever.Commands
{
    public class UpdateRestaurantCommand
    {
        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public string Uri { get; set; }

        public string XPath { get; set; }

        public ScrapeResultType ResultType { get; set; }
    }
}