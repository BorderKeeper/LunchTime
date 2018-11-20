using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using LunchTime.Core.Api.Common.Queries;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.Entities;

namespace LunchTime.Services.Controllers
{
    [Route("api/RestaurantMenuCollection")]
    public class RestaurantMenuCollectionController : ApiController
    {
        private readonly IRestaurantMenuCollectionQueryHandler _menuCollectionQueryHandler;

        public RestaurantMenuCollectionController(IRestaurantMenuCollectionQueryHandler menuCollectionQueryHandler)
        {
            _menuCollectionQueryHandler = menuCollectionQueryHandler;
        }

        public async Task<IEnumerable<RestaurantMenuListItem>> Get()
        {
            return await _menuCollectionQueryHandler.Execute(new EmptyQuery());
        }
    }
}
