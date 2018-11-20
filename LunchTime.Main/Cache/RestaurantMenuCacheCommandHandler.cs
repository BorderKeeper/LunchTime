using LunchTime.Main.Api.Cache;
using LunchTime.Main.Api.Cache.Commands;
using LunchTime.Main.Cache.DataAccess;

namespace LunchTime.Main.Cache
{
    public class RestaurantMenuCacheCommandHandler : IRestaurantMenuCacheCommandHandler
    {
        private readonly ICachedMenuWriter _cachedMenuWriter;

        public RestaurantMenuCacheCommandHandler()
        {
            _cachedMenuWriter = new CachedMenuWriter();
        }

        public void Execute(RestaurantMenuCacheCommand command)
        {
            _cachedMenuWriter.CacheMenu(command.RestaurantMenu.RestaurantId, command.RestaurantMenu.HtmlMenu);
        }
    }
}