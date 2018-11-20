using System.Collections.Generic;
using LunchTime.Core.Api.Common;
using LunchTime.Main.Api.Cache.Queries;
using LunchTime.Main.Api.Retriever.Entities;

namespace LunchTime.Main.Api.Cache
{
    public interface IRestaurantMenuCacheQueryHandler : IQueryHandler<RestaurantMenuCacheQuery, IEnumerable<RestaurantMenu>>
    {       
    }
}