using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LunchTime.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LunchTime.Web.Controllers
{
    public class RestaurantMenuCollectionController : ServiceController
    {
        private const string ServiceCall = "api/RestaurantMenuCollection";

        public async Task<IActionResult> Index()
        {
            List<RestaurantMenuModel> learnedRestaurants = new List<RestaurantMenuModel>();

            using (var client = SetupHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ServiceCall);

                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = response.Content.ReadAsStringAsync().Result;

                    learnedRestaurants = JsonConvert.DeserializeObject<List<RestaurantMenuModel>>(rawResponse);
                }
            }

            return View(learnedRestaurants);
        }
    }
}