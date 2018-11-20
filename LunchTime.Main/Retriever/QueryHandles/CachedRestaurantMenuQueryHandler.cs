using System.Linq;
using System.Threading.Tasks;
using LunchTime.Main.Api.Cache;
using LunchTime.Main.Api.Cache.Commands;
using LunchTime.Main.Api.Cache.Queries;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Main.Api.Retriever.QueryHandlers;

namespace LunchTime.Main.Retriever.QueryHandles
{
    public class CachedRestaurantMenuQueryHandler : ICachedRestaurantMenuQueryHandler
    {
        private readonly IRestaurantMenuCacheQueryHandler _menuCacheQueryHandler;
        private readonly IRestaurantMenuCacheCommandHandler _cacheMenuCommandHandler;
        private readonly IRestaurantMenuQueryHandler _restaurantMenuQueryHandler;

        public CachedRestaurantMenuQueryHandler(IRestaurantMenuCacheQueryHandler menuCacheQueryHandler, 
            IRestaurantMenuCacheCommandHandler cacheMenuCommandHandler, 
            IRestaurantMenuQueryHandler restaurantMenuQueryHandler)
        {
            _menuCacheQueryHandler = menuCacheQueryHandler;
            _cacheMenuCommandHandler = cacheMenuCommandHandler;
            _restaurantMenuQueryHandler = restaurantMenuQueryHandler;
        }

        public async Task<RestaurantMenu> Execute(CachedRestaurantMenuQuery query)
        {
            var cachedWebsite = _menuCacheQueryHandler.Execute(new RestaurantMenuCacheQuery
            {
                RestaurantId = query.RestaurantId
            });

            if (cachedWebsite.Any())
            {
                return cachedWebsite.Single();
            }

            var menu = await _restaurantMenuQueryHandler.Execute(new RestaurantMenuQuery()
            {
                RestaurantId = query.RestaurantId
            });

            CacheWebsite(menu);

            return menu;
        }

        private void CacheWebsite(RestaurantMenu menu)
        {
            _cacheMenuCommandHandler.Execute(new RestaurantMenuCacheCommand
            {
                RestaurantMenu = menu
            });
        }
    }
}