using System;
using System.Collections.Generic;
using System.Web.Http;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Main.Api.Retriever.QueryHandlers;

namespace LunchTime.Services.Controllers
{
    public class RestaurantMenuController : ApiController
    {
        private readonly ICachedRestaurantMenuQueryHandler _cachedRestaurantMenuQueryHandler;

        public RestaurantMenuController(ICachedRestaurantMenuQueryHandler cachedRestaurantMenuQueryHandler)
        {
            _cachedRestaurantMenuQueryHandler = cachedRestaurantMenuQueryHandler;
        }

        [Route("api/RestaurantMenu/{restaurantId}")]
        public IEnumerable<RestaurantMenu> Get(int restaurantId)
        {
            var result = _cachedRestaurantMenuQueryHandler.Execute(new CachedRestaurantMenuQuery
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
