using System.Threading.Tasks;
using LunchTime.Database.LunchTime;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.Commands;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Main.Retriever.DataAccess;
using LunchTime.Scraper.Api.Scapers.Entities.ScrapeTargets;

namespace LunchTime.Main.Retriever
{
    public class LearnRestaurantCommandHandler : ILearnRestaurantCommandHandler
    {
        private readonly IRestaurantReader _restaurantReader;
        private readonly IRestaurantWriter _restaurantWriter;
        private readonly IRestaurantMenuQueryHandler _menuCacher;

        public LearnRestaurantCommandHandler(IRestaurantMenuQueryHandler menuCacher)
        {
            _menuCacher = menuCacher;
            _restaurantReader = new RestaurantReader();
            _restaurantWriter = new RestaurantWriter();
        }

        public int Execute(LearnRestaurantCommand command)
        {
            var restaurant = _restaurantReader.GetRestaurant(command.Uri, command.XPath);

            var learnedRestaurant = new LearnedRestaurant
            {
                RestaurantName = command.RestaurantName,
                Uri = command.Uri,
                XPath = command.XPath,                   
                ResultType = command.ResultType,
                TargetType = ScrapeTargetType.Naive 
                //TODO: This needs to be automatic when smart is implemented
            };

            if (restaurant != null)
            {
                learnedRestaurant.RestaurantId = restaurant.RestaurantId;
                _restaurantWriter.UpdateRestaurant(learnedRestaurant);
            }
            else
            {
                learnedRestaurant.RestaurantId = _restaurantWriter.SaveRestaurant(learnedRestaurant);
            }

            CacheMenu(learnedRestaurant.RestaurantId);

            return learnedRestaurant.RestaurantId;
        }

        public async Task CacheMenu(int restaurantId)
        {
            await Task.Run(() =>
            {
                _menuCacher.Execute(new RestaurantMenuQuery
                {
                    RestaurantId = restaurantId
                });
            });
        }
    }
}