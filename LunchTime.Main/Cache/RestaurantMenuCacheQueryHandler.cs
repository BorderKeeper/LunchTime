using System.Collections.Generic;
using LunchTime.Database.LunchTime;
using LunchTime.Main.Api.Cache;
using LunchTime.Main.Api.Cache.Queries;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Main.Cache.DataAccess;

namespace LunchTime.Main.Cache
{
    public class RestaurantMenuCacheQueryHandler : IRestaurantMenuCacheQueryHandler
    {
        private readonly ICachedMenuReader _cachedMenuReader;

        public RestaurantMenuCacheQueryHandler()
        {
            _cachedMenuReader = new CachedMenuReader();
        }

        public IEnumerable<RestaurantMenu> Execute(RestaurantMenuCacheQuery query)
        {
            List<RestaurantMenu> restaurantMenus = new List<RestaurantMenu>();

            CachedMenu cachedMenu = _cachedMenuReader.GetCachedMenu(query.RestaurantId);

            if (cachedMenu != default(CachedMenu))
            {
                restaurantMenus.Add(new RestaurantMenu
                {
                    RestaurantId = query.RestaurantId,
                    HtmlMenu = cachedMenu.Menu
                });
            }

            return restaurantMenus;
        }
    }
}