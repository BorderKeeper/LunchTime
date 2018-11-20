using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LunchTime.Core.Api.Common.Queries;
using LunchTime.Main.Api.Retriever;
using LunchTime.Services.Models;

namespace LunchTime.Services.Controllers
{
    [Route("api/RestaurantCollection")]
    public class RestaurantCollectionController : ApiController
    {
        private readonly ILearnedRestaurantCollectionQueryHandler _learnedRestaurantsQueryHandler;

        public RestaurantCollectionController(ILearnedRestaurantCollectionQueryHandler learnedRestaurantsQueryHandler)
        {
            _learnedRestaurantsQueryHandler = learnedRestaurantsQueryHandler;
        }

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
    }
}
