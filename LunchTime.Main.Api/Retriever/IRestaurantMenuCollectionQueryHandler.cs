using System.Collections.Generic;
using System.Threading.Tasks;
using LunchTime.Core.Api.Common;
using LunchTime.Core.Api.Common.Queries;
using LunchTime.Main.Api.Retriever.Entities;

namespace LunchTime.Main.Api.Retriever
{
    public interface IRestaurantMenuCollectionQueryHandler : IQueryHandler<EmptyQuery, Task<IEnumerable<RestaurantMenuListItem>>>
    {
    }
}