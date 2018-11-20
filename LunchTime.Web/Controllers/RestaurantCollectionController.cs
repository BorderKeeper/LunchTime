using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LunchTime.Web.Models;
using LunchTime.Web.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LunchTime.Web.Controllers
{
    public class RestaurantCollectionController : ServiceController
    {
        private const string IndexServiceCall = "api/RestaurantCollection";

        private const string DeleteServiceCall = "api/DeleteRestaurant";

        private const string UpdateServiceCall = "api/UpdateRestaurant";

        public async Task<IActionResult> Index()
        {
            var restaurants = await GetRestaurants();

            return View(restaurants);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int restaurantId)
        {
            var restaurant = await GetRestaurant(restaurantId);

            return View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantModel model)
        {
            using (var client = SetupHttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(UpdateServiceCall, model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int restaurantId)
        {
            using (var client = SetupHttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(DeleteServiceCall + "/" + restaurantId);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        private async Task<List<RestaurantModel>> GetRestaurants()
        {
            List<RestaurantModel> learnedRestaurants = new List<RestaurantModel>();

            using (var client = SetupHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(IndexServiceCall);

                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = await response.Content.ReadAsStringAsync();

                    learnedRestaurants = JsonConvert.DeserializeObject<List<RestaurantModel>>(rawResponse);
                }

                return learnedRestaurants;
            }
        }

        private async Task<RestaurantModel> GetRestaurant(int restaurantId)
        {
            using (var client = SetupHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(IndexServiceCall + "/" + restaurantId);

                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<RestaurantModel>(rawResponse);
                }

                return new RestaurantModel();
            }
        }

    }
}