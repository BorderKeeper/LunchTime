using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LunchTime.Main.Api.Retriever.CommandHandlers;
using LunchTime.Main.Api.Retriever.Commands;
using LunchTime.Scraper.Api.Scapers.Entities;
using LunchTime.Services.Models;
using Newtonsoft.Json;

namespace LunchTime.Services.Controllers
{
    [Route("api/LearnRestaurant")]   
    public class LearnRestaurantController : ApiController
    {
        private readonly ILearnRestaurantCommandHandler _learnRestaurantCommandHandler;

        public LearnRestaurantController(ILearnRestaurantCommandHandler learnRestaurantCommandHandler)
        {
            _learnRestaurantCommandHandler = learnRestaurantCommandHandler;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(RestaurantModel model)
        {           
            if (model == default(RestaurantModel))
            {
                model = GetFromJsonBody();
            }

            try
            {
                ValidateModel(model);

                var restaurantId = await _learnRestaurantCommandHandler.Execute(new LearnRestaurantCommand
                {
                    RestaurantName = model.Name,
                    Uri = model.Uri,
                    XPath = model.XPath,
                    ResultType = (ScrapeResultType) Enum.Parse(typeof(ScrapeResultType), model.ResultType)
                });

                return Request.CreateResponse(HttpStatusCode.OK, restaurantId);
            }
            catch (ValidationException validation)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, validation.Message);
            }
        }

        private void ValidateModel(RestaurantModel model)
        {
            ScrapeResultType type;

            if(model is null) throw new ValidationException("Model is missing");
            if(model.Name is null || model.Name.Length == 0) throw new ValidationException("Restaurant name is missing");
            if(model.Uri is null || model.Uri.Length == 0) throw new ValidationException("Uri is missing");
            if(model.XPath is null || model.XPath.Length == 0) throw new ValidationException("XPath is missing");
            if(model.ResultType is null || model.XPath.Length == 0) throw new ValidationException("ResultType is missing");
            if(!Enum.TryParse(model.ResultType, out type)) throw new ValidationException("ResultType is invalid");
        }

        private RestaurantModel GetFromJsonBody()
        {
            //TODO: Works in postman, but needs below when called via page
            var modelReally = Request.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<RestaurantModel>(modelReally);
        }
    }
}
