using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LunchTime.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LunchTime.Web.Controllers
{
    public class RestaurantCollectionController : ServiceController
    {
        private const string IndexServiceCall = "api/RestaurantCollection";

        private const string DeleteServiceCall = "api/DeleteRestaurant";

        public async Task<IActionResult> Index()
        {
            List<RestaurantModel> learnedRestaurants = new List<RestaurantModel>();

            using (var client = SetupHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(IndexServiceCall);

                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = response.Content.ReadAsStringAsync().Result;

                    learnedRestaurants = JsonConvert.DeserializeObject<List<RestaurantModel>>(rawResponse);
                }

                return View(learnedRestaurants);
            }
        }

        public async Task<IActionResult> Edit()
        {
            //TODO: Redirect to create with stuff populated

            return View();
        }

        public async Task<IActionResult> Delete(int restaurantId)
        {
            return View();
        }
    }
}