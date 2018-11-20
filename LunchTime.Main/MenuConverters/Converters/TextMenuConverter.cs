using System.Threading.Tasks;
using LunchTime.Main.Api.MenuConverters;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Scraper.Api.Scapers.Entities;

namespace LunchTime.Main.MenuConverters.Converters
{
    public class TextMenuConverter : IMenuConverter
    {
        public Task<RestaurantMenu> Convert(int restaurantId, ScrapeResult scrapeResult)
        {
            var menu = new RestaurantMenu
            {
                RestaurantId = restaurantId,
                HtmlMenu = scrapeResult.Result
            };

            return Task.FromResult(menu);
        }
    }
}