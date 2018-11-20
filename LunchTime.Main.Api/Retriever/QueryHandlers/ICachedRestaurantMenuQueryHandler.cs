using System.Threading.Tasks;
using LunchTime.Core.Api.Common;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Main.Api.Retriever.Queries;

namespace LunchTime.Main.Api.Retriever.QueryHandlers
{
    public interface ICachedRestaurantMenuQueryHandler : IQueryHandler<CachedRestaurantMenuQuery, Task<RestaurantMenu>>
    {        
    }
}