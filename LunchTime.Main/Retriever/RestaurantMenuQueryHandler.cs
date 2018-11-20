using System;
using System.Linq;
using System.Threading.Tasks;
using LunchTime.Main.Api.Cache;
using LunchTime.Main.Api.Cache.Commands;
using LunchTime.Main.Api.Cache.Queries;
using LunchTime.Main.Api.ImageRecognition;
using LunchTime.Main.Api.ImageRecognition.Queries;
using LunchTime.Main.Api.Retriever;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Scraper.Api.Scapers;
using LunchTime.Scraper.Api.Scapers.Entities;
using LunchTime.Scraper.Api.Scapers.Entities.ScrapeTargets;
using LunchTime.Scraper.Api.Scapers.Queries;

namespace LunchTime.Main.Retriever
{
    public class RestaurantMenuQueryHandler : IRestaurantMenuQueryHandler
    {
        private readonly IScrapePageContentQueryHandler _scrapePage;
        private readonly ILearnedRestaurantQueryHandler _learnedRestaurant;
        private readonly IRestaurantMenuCacheQueryHandler _menuCacheQueryHandler;
        private readonly IRestaurantMenuCacheCommandHandler _cacheMenuCommandHandler;
        private readonly IConvertToTextQueryHandler _convertToText;

        public RestaurantMenuQueryHandler(IScrapePageContentQueryHandler scrapePage, 
            ILearnedRestaurantQueryHandler learnedRestaurant,
            IRestaurantMenuCacheQueryHandler menuCacheQueryHandler, 
            IRestaurantMenuCacheCommandHandler cacheMenuCommandHandler, 
            IConvertToTextQueryHandler convertToText)
        {
            _scrapePage = scrapePage;
            _learnedRestaurant = learnedRestaurant;
            _menuCacheQueryHandler = menuCacheQueryHandler;
            _cacheMenuCommandHandler = cacheMenuCommandHandler;
            _convertToText = convertToText;
        }

        public async Task<RestaurantMenu> Execute(RestaurantMenuQuery query)
        {
            var cachedWebsite = _menuCacheQueryHandler.Execute(new RestaurantMenuCacheQuery
            {
                RestaurantId = query.RestaurantId
            });

            if (cachedWebsite.Any())
            {
                return cachedWebsite.Single();
            }

            var menu = await LoadMenu(query.RestaurantId);

            CacheWebsite(menu);

            return menu;
        }

        private async Task<RestaurantMenu> LoadMenu(int restaurantId)
        {
            var learnedRestaurant = _learnedRestaurant.Execute(new LearnedRestaurantQuery
            {
                RestaurantId = restaurantId
            });

            var scrapedPage = await _scrapePage.Execute(new ScrapePageContentQuery
            {
                Target = new NaiveScrapeTarget(learnedRestaurant.XPath),
                WebsiteUri = new Uri(learnedRestaurant.Uri),
                ScrapeResultType = learnedRestaurant.ResultType
            });

            return ConvertScrapedPageToMenu(restaurantId, scrapedPage);
        }

        private RestaurantMenu ConvertScrapedPageToMenu(int restaurantId, ScrapeResult scrapeResult)
        {
            RestaurantMenu menu = scrapeResult.ResultyType == ScrapeResultType.Text
                ? ExtractHtml(restaurantId, scrapeResult.Result)
                : ExtractImageHtml(restaurantId, scrapeResult.Result);

            return menu;
        }

        private RestaurantMenu ExtractImageHtml(int restaurantId, string address)
        {
            var resultText = _convertToText.Execute(new ConvertToTextQuery
            {
                Uri = new Uri(address)
            });

            return new RestaurantMenu
            {
                HtmlMenu = resultText,
                RestaurantId = restaurantId
            };
        }

        private RestaurantMenu ExtractHtml(int restaurantId, string html)
        {
            return new RestaurantMenu
            {
                RestaurantId = restaurantId,
                HtmlMenu = html
            };
        }

        private void CacheWebsite(RestaurantMenu menu)
        {
            _cacheMenuCommandHandler.Execute(new RestaurantMenuCacheCommand
            {
                RestaurantMenu = menu
            });
        }
    }
}