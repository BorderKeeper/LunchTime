using LunchTime.Core.Api.Common;
using LunchTime.Database.LunchTime;
using LunchTime.Main.Api.Retriever.Queries;

namespace LunchTime.Main.Api.Retriever.QueryHandlers
{
    public interface ILearnedRestaurantQueryHandler : IQueryHandler<LearnedRestaurantQuery, LearnedRestaurant>
    {        
    }
}