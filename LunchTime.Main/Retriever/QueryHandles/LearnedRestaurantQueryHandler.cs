using System.Collections.Generic;
using LunchTime.Database.LunchTime;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Main.Api.Retriever.QueryHandlers;
using LunchTime.Main.Retriever.DataAccess;

namespace LunchTime.Main.Retriever.QueryHandles
{
    public class LearnedRestaurantQueryHandler : ILearnedRestaurantQueryHandler
    {
        private readonly IRestaurantReader _restaurantReader;

        public LearnedRestaurantQueryHandler()
        {
            _restaurantReader = new RestaurantReader();
        }

        public LearnedRestaurant Execute(LearnedRestaurantQuery query)
        {
            var restaurant = _restaurantReader.GetRestaurant(query.RestaurantId);

            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant was not found in the database");
            }

            return restaurant;
        }
    }
}