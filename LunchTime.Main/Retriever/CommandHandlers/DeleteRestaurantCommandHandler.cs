using LunchTime.Core.Api.Common.Enums;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.CommandHandlers;
using LunchTime.Main.Api.Retriever.Commands;
using LunchTime.Main.Cache.DataAccess;
using LunchTime.Main.Retriever.DataAccess;

namespace LunchTime.Main.Retriever.CommandHandlers
{
    public class DeleteRestaurantCommandHandler : IDeleteRestaurantCommandHandler
    {
        private readonly IRestaurantWriter _restaurantWriter;
        private readonly ICachedMenuWriter _cachedMenuWriter;

        public DeleteRestaurantCommandHandler()
        {
            _restaurantWriter = new RestaurantWriter();
            _cachedMenuWriter = new CachedMenuWriter();
        }

        public DeletionStatus Execute(DeleteRestaurantCommand command)
        {
            _cachedMenuWriter.DeleteCachedMenu(command.RestaurantId);
            return _restaurantWriter.DeleteRestaurant(command.RestaurantId);
        }
    }
}