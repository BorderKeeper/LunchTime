using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LunchTime.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunchTime.Web.Controllers
{
    public class AddRestaurantController : ServiceController
    {
        private const string ServiceCall = "api/LearnRestaurant";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(RestaurantModel model)
        {
            if (ValidateModel(model))
            {
                using (var client = SetupHttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(ServiceCall, model);

                    return View();
                } 
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

                return View();
            }
        }

        private bool ValidateModel(RestaurantModel model)
        {
            if (model is null) return false;

            return !(model.Name == string.Empty
                || model.Uri == string.Empty
                || model.XPath == string.Empty);
        }
    }
}