using System;
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
    public class LearnRestaurantCommandHandler : ILearnRestaurantCommandHandler
    {
        private readonly IRestaurantReader _restaurantReader;
        private readonly IRestaurantWriter _restaurantWriter;
        private readonly ICacheNewRestaurantCommandHandler _cacheNewRestaurantCommandHandler;

        public LearnRestaurantCommandHandler(ICacheNewRestaurantCommandHandler cacheNewRestaurantCommandHandler)
        {
            _cacheNewRestaurantCommandHandler = cacheNewRestaurantCommandHandler;
            _restaurantReader = new RestaurantReader();
            _restaurantWriter = new RestaurantWriter();
        }

        public async Task<int> Execute(LearnRestaurantCommand command)
        {
            var restaurant = _restaurantReader.GetRestaurant(command.Uri, command.XPath);

            if (restaurant != default(LearnedRestaurant))
            {
                throw new InvalidOperationException($"Specified restaurant with url \"{command.Uri}\" and xpath \"{command.XPath}\" already exists.");
            }

            var learntRestaurant = new LearnedRestaurant
            {
                RestaurantName = command.RestaurantName,
                Uri = command.Uri,
                XPath = command.XPath,                   
                ResultType = command.ResultType,
                TargetType = ScrapeTargetType.Naive 
                //TODO: This needs to be automatic when smart is implemented
            };

            learntRestaurant.RestaurantId = _restaurantWriter.SaveRestaurant(learntRestaurant);

            try
            {
                return learntRestaurant.RestaurantId;
            }
            finally
            {
                await CacheMenu(learntRestaurant.RestaurantId);
            }           
        }

        public async Task CacheMenu(int restaurantId)
        {
            await _cacheNewRestaurantCommandHandler.Execute(new CacheNewRestaurantCommand()
            {
                RestaurantId = restaurantId
            });
        }
    }
}