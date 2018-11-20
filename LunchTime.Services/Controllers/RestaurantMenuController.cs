using System;
using System.Collections.Generic;
using System.Web.Http;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Main.Api.Retriever.Queries;

namespace LunchTime.Services.Controllers
{
    public class RestaurantMenuController : ApiController
    {
        private readonly IRestaurantMenuQueryHandler _restaurantMenuQueryHandler;

        public RestaurantMenuController(IRestaurantMenuQueryHandler restaurantMenuQueryHandler)
        {
            _restaurantMenuQueryHandler = restaurantMenuQueryHandler;
        }

        [Route("api/RestaurantMenu/{restaurantId}")]
        public IEnumerable<RestaurantMenu> Get(int restaurantId)
        {
            var result = _restaurantMenuQueryHandler.Execute(new RestaurantMenuQuery
            {
                RestaurantId = restaurantId
            });

            result.Wait(TimeSpan.FromSeconds(5));

            return result.Result == default(RestaurantMenu) ? 
                new List<RestaurantMenu>() : 
                new List<RestaurantMenu> { result.Result };
        }     
    }
}
