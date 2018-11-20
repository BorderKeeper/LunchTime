using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using LunchTime.Scraper.Api.Scapers.Entities;
using LunchTime.Scraper.Api.Scapers.Entities.ScrapeTargets;

namespace LunchTime.Database.LunchTime
{
    public class LearnedRestaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public string Uri { get; set; }

        public string XPath { get; set; }

        public ScrapeTargetType TargetType { get; set; }

        public ScrapeResultType ResultType { get; set; }

        public bool RequiresRecaching(LearnedRestaurant newRestaurant)
        {
            return !newRestaurant.Uri.Equals(Uri) ||
                !newRestaurant.XPath.Equals(XPath) ||
                !newRestaurant.TargetType.Equals(TargetType) ||
                !newRestaurant.ResultType.Equals(ResultType);
        }
    }

    public class LearnedRestaurantContext : DbContext
    {
        public LearnedRestaurantContext() : base("LunchTime")
        {
        }

        public DbSet<LearnedRestaurant> LearnedRestaurants { get; set; }
    }
}