using System.Collections.Generic;
using LunchTime.Core.Api.Common;
using LunchTime.Core.Api.Common.Queries;
using LunchTime.Database.LunchTime;

namespace LunchTime.Main.Api.Retriever.QueryHandlers
{
    public interface ILearnedRestaurantCollectionQueryHandler : IQueryHandler<EmptyQuery, IList<LearnedRestaurant>>
    {       
    }
}