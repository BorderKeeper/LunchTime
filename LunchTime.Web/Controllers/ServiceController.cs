using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace LunchTime.Web.Controllers
{
    public class ServiceController : Controller
    {
        protected HttpClient SetupHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ServicesConfiguration.Url)
            };

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ServicesConfiguration.Type));

            return client;
        }
    }
}