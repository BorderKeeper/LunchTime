using System.Threading.Tasks;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Scraper.Api.Scapers.Entities;

namespace LunchTime.Main.Api.MenuConverters
{
    public interface IMenuConverter
    {
        Task<RestaurantMenu> Convert(int restaurantId, ScrapeResult scrapeResult);
    }
}