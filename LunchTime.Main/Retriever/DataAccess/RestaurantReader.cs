using System.Collections.Generic;
using System.Linq;
using LunchTime.Database.LunchTime;

namespace LunchTime.Main.Retriever.DataAccess
{
    public interface IRestaurantReader
    {
        LearnedRestaurant GetRestaurant(int restaurantId);

        LearnedRestaurant GetRestaurant(string uri, string xPath);

        IList<LearnedRestaurant> GetRestaurants();
    }

    public class RestaurantReader : IRestaurantReader
    {
        public LearnedRestaurant GetRestaurant(int restaurantId)
        {
            using (var scrapeTargetTable = new LearnedRestaurantContext())
            {
                return scrapeTargetTable.LearnedRestaurants.FirstOrDefault(target => target.RestaurantId == restaurantId);
            }
        }

        public LearnedRestaurant GetRestaurant(string uri, string xPath)
        {
            using (var scrapeTargetTable = new LearnedRestaurantContext())
            {
                return scrapeTargetTable.LearnedRestaurants.FirstOrDefault(target => target.Uri.Equals(uri) && target.XPath.Equals(xPath));
            }
        }

        public IList<LearnedRestaurant> GetRestaurants()
        {
            using (var scrapeTargetTable = new LearnedRestaurantContext())
            {
                return scrapeTargetTable.LearnedRestaurants.ToList();
            }
        }
    }
}