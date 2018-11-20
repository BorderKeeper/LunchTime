using System.Threading.Tasks;
using LunchTime.Main.Api.Cache;
using LunchTime.Main.Api.Cache.Commands;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Main.Api.Retriever.QueryHandlers;

namespace LunchTime.Main.Cache
{
    public class CacheNewRestaurantCommandHandler : ICacheNewRestaurantCommandHandler
    {
        private readonly IRestaurantMenuQueryHandler _restaurantMenuQueryHandler;
        private readonly IRestaurantMenuCacheCommandHandler _cacheMenuCommandHandler;

        public CacheNewRestaurantCommandHandler(IRestaurantMenuQueryHandler restaurantMenuQueryHandler, 
            IRestaurantMenuCacheCommandHandler cacheMenuCommandHandler)
        {
            _restaurantMenuQueryHandler = restaurantMenuQueryHandler;
            _cacheMenuCommandHandler = cacheMenuCommandHandler;
        }

        public async Task Execute(CacheNewRestaurantCommand command)
        {
            var menu = await _restaurantMenuQueryHandler.Execute(new RestaurantMenuQuery()
            {
                RestaurantId = command.RestaurantId
            });

            _cacheMenuCommandHandler.Execute(new RestaurantMenuCacheCommand
            {
                RestaurantMenu = menu
            });
        }
    }
}