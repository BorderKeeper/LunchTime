using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LunchTime.Core.Api.Common.Queries;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Main.Api.Retriever.QueryHandlers;
using LunchTime.Services.Models;

namespace LunchTime.Services.Controllers
{
    [Route("api/RestaurantCollection")]
    [Route("api/RestaurantCollection/{restaurantId}")]
    public class RestaurantCollectionController : ApiController
    {
        private readonly ILearnedRestaurantCollectionQueryHandler _learnedRestaurantsQueryHandler;
        private readonly ILearnedRestaurantQueryHandler _learnedRestaurantQueryHandler;

        public RestaurantCollectionController(ILearnedRestaurantCollectionQueryHandler learnedRestaurantsQueryHandler, 
            ILearnedRestaurantQueryHandler learnedRestaurantQueryHandler)
        {
            _learnedRestaurantsQueryHandler = learnedRestaurantsQueryHandler;
            _learnedRestaurantQueryHandler = learnedRestaurantQueryHandler;
        }

        [HttpGet]
        public IEnumerable<RestaurantModel> Get()
        {
            var learnedRestaurants = _learnedRestaurantsQueryHandler.Execute(new EmptyQuery()).ToList();

            return learnedRestaurants.Select(restaurant => new RestaurantModel
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.RestaurantName,
                ResultType = restaurant.ResultType.ToString(),
                Uri = restaurant.Uri,
                XPath = restaurant.XPath
            });
        }

        [HttpGet]
        public RestaurantModel Get(int restaurantId)
        {
            var learnedRestaurant = _learnedRestaurantQueryHandler.Execute(new LearnedRestaurantQuery
            {
                RestaurantId = restaurantId
            });

            return new RestaurantModel
            {
                RestaurantId = learnedRestaurant.RestaurantId,
                Name = learnedRestaurant.RestaurantName,
                ResultType = learnedRestaurant.ResultType.ToString(),
                Uri = learnedRestaurant.Uri,
                XPath = learnedRestaurant.XPath
            };
        }
    }
}
