using LunchTime.Web.Models.Enums;

namespace LunchTime.Web.Models
{
    public class CreateRestaurantModel
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public string XPath { get; set; }

        public ScrapeResultType ResultType { get; set; }
    }
}