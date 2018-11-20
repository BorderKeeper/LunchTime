using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchTime.Core.Api.Common.Queries;
using LunchTime.Database.LunchTime;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Main.Api.Retriever.QueryHandlers;
using LunchTime.Main.Retriever.DataAccess;

namespace LunchTime.Main.Retriever.QueryHandles
{
    public class RestaurantMenuCollectionQueryHandler : IRestaurantMenuCollectionQueryHandler
    {
        private readonly IRestaurantReader _restaurantReader;
        private readonly ICachedRestaurantMenuQueryHandler _menuQueryHandler;

        public RestaurantMenuCollectionQueryHandler(ICachedRestaurantMenuQueryHandler menuQueryHandler)
        {
            _restaurantReader = new RestaurantReader();
            _menuQueryHandler = menuQueryHandler;
        }

        public async Task<IEnumerable<RestaurantMenuListItem>> Execute(EmptyQuery query)
        {
            List<Task<RestaurantMenuListItem>> menus = new List<Task<RestaurantMenuListItem>>();

            IList<LearnedRestaurant> restaurants = _restaurantReader.GetRestaurants();

            foreach (LearnedRestaurant restaurant in restaurants)
            {
                menus.Add(GetRestaurantMenu(restaurant));
            }

            await Task.WhenAll(menus.ToArray());

            return menus.Select(task => task.Result);
        }

        private async Task<RestaurantMenuListItem> GetRestaurantMenu(LearnedRestaurant restaurant)
        {
            RestaurantMenu menu = await _menuQueryHandler.Execute(new CachedRestaurantMenuQuery
            {
                RestaurantId = restaurant.RestaurantId
            });

            return new RestaurantMenuListItem
            {
                HtmlMenu = menu.HtmlMenu,
                RestaurantId = menu.RestaurantId,
                RestaurantName = restaurant.RestaurantName
            };
        }
    }
}