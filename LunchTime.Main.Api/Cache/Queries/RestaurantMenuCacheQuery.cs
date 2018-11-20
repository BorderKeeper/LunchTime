using LunchTime.Main.Api.Retriever.Entities;

namespace LunchTime.Main.Api.Cache.Queries
{
    public class RestaurantMenuCacheQuery
    {
        public int RestaurantId { get; set; }

        public RestaurantMenu RestaurantMenu { get; set; }
    }
}