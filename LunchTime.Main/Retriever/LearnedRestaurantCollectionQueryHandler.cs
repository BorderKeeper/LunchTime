using System.Collections.Generic;
using LunchTime.Core.Api.Common.Queries;
using LunchTime.Database.LunchTime;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Retriever.DataAccess;

namespace LunchTime.Main.Retriever
{
    public class LearnedRestaurantCollectionQueryHandler : ILearnedRestaurantCollectionQueryHandler
    {
        private readonly IRestaurantReader _reader;

        public LearnedRestaurantCollectionQueryHandler()
        {
            _reader = new RestaurantReader();
        }

        public IList<LearnedRestaurant> Execute(EmptyQuery query)
        {
            return _reader.GetRestaurants();
        }
    }
}