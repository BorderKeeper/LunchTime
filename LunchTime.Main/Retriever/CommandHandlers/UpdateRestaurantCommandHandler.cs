using System.Threading.Tasks;
using LunchTime.Database.LunchTime;
using LunchTime.Main.Api.Cache;
using LunchTime.Main.Api.Cache.Commands;
using LunchTime.Main.Api.Retriever.CommandHandlers;
using LunchTime.Main.Api.Retriever.Commands;
using LunchTime.Main.Retriever.DataAccess;
using LunchTime.Scraper.Api.Scapers.Entities.ScrapeTargets;

namespace LunchTime.Main.Retriever.CommandHandlers
{
    public class UpdateRestaurantCommandHandler : IUpdateRestaurantCommandHandler
    {
        private readonly IRestaurantReader _restaurantReader;
        private readonly IRestaurantWriter _restaurantWriter;
        private readonly ICacheNewRestaurantCommandHandler _cacheNewRestaurantCommandHandler;

        public UpdateRestaurantCommandHandler(ICacheNewRestaurantCommandHandler cacheNewRestaurantCommandHandler)
        {
            _restaurantWriter = new RestaurantWriter();
            _restaurantReader = new RestaurantReader();
            _cacheNewRestaurantCommandHandler = cacheNewRestaurantCommandHandler;           
        }

        public async Task Execute(UpdateRestaurantCommand command)
        {
            var storedRestaurant = _restaurantReader.GetRestaurant(command.RestaurantId);
            var newRestaurant = GetLearnedRestaurant(command);

            _restaurantWriter.UpdateRestaurant(newRestaurant);

            if (storedRestaurant.RequiresRecaching(newRestaurant))
            {
                await _cacheNewRestaurantCommandHandler.Execute(new CacheNewRestaurantCommand
                {
                    RestaurantId = command.RestaurantId
                });
            }
        }

        private LearnedRestaurant GetLearnedRestaurant(UpdateRestaurantCommand command)
        {
            return new LearnedRestaurant
            {
                RestaurantId = command.RestaurantId,
                RestaurantName = command.RestaurantName,
                Uri = command.Uri,
                XPath = command.XPath,
                ResultType = command.ResultType,
                TargetType = ScrapeTargetType.Naive
                //TODO: This needs to be automatic when smart is implemented
            };
        }
    }
}