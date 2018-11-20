using System.Net;
using System.Net.Http;
using System.Web.Http;
using LunchTime.Core.Api.Common.Enums;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.CommandHandlers;
using LunchTime.Main.Api.Retriever.Commands;

namespace LunchTime.Services.Controllers
{
    [Route("api/DeleteRestaurant/{restaurantId}")]
    public class DeleteRestaurantController : ApiController
    {
        private readonly IDeleteRestaurantCommandHandler _deleteRestaurantCommandHandler;

        public DeleteRestaurantController(IDeleteRestaurantCommandHandler deleteRestaurantCommandHandler)
        {
            _deleteRestaurantCommandHandler = deleteRestaurantCommandHandler;
        }
       
        [HttpDelete]
        public HttpResponseMessage Delete(int restaurantId)
        {
            var status = _deleteRestaurantCommandHandler.Execute(new DeleteRestaurantCommand
            {
                RestaurantId = restaurantId
            });

            return status == DeletionStatus.Deleted ? 
                Request.CreateResponse(HttpStatusCode.OK, $"Restaurant with an id {restaurantId} was deleted sucessfully") : 
                Request.CreateResponse(HttpStatusCode.NotFound, $"Restaurant with an id {restaurantId} was not found");
        }
    }
}
