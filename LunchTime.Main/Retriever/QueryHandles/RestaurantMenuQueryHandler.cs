using System;
using System.Threading.Tasks;
using LunchTime.Main.Api.MenuConverters;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Main.Api.Retriever.Queries;
using LunchTime.Main.Api.Retriever.QueryHandlers;
using LunchTime.Scraper.Api.Scapers;
using LunchTime.Scraper.Api.Scapers.Entities;
using LunchTime.Scraper.Api.Scapers.Entities.ScrapeTargets;
using LunchTime.Scraper.Api.Scapers.Queries;

namespace LunchTime.Main.Retriever.QueryHandles
{
    public class RestaurantMenuQueryHandler : IRestaurantMenuQueryHandler
    {
        private readonly IScrapePageContentQueryHandler _scrapePage;
        private readonly ILearnedRestaurantQueryHandler _learnedRestaurant;
        private readonly IMenuConverterFactory _menuConverterFactory;

        public RestaurantMenuQueryHandler(IScrapePageContentQueryHandler scrapePage, 
            ILearnedRestaurantQueryHandler learnedRestaurant,
            IMenuConverterFactory menuConverterFactory)
        {
            _scrapePage = scrapePage;
            _learnedRestaurant = learnedRestaurant;
            _menuConverterFactory = menuConverterFactory;
        }

        public async Task<RestaurantMenu> Execute(RestaurantMenuQuery query)
        {
            var learnedRestaurant = _learnedRestaurant.Execute(new LearnedRestaurantQuery
            {
                RestaurantId = query.RestaurantId
            });

            ScrapeResult scrapedPage = await _scrapePage.Execute(new ScrapePageContentQuery
            {
                Target = new NaiveScrapeTarget(learnedRestaurant.XPath),
                WebsiteUri = new Uri(learnedRestaurant.Uri),
                ScrapeResultType = learnedRestaurant.ResultType
            });

            var menuConverter = _menuConverterFactory.GetConverter(scrapedPage.ResultyType);

            return await menuConverter.Convert(query.RestaurantId, scrapedPage);
        }
    }
}