using System;
using System.Threading.Tasks;
using LunchTime.Main.Api.ImageRecognition;
using LunchTime.Main.Api.ImageRecognition.Queries;
using LunchTime.Main.Api.MenuConverters;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Scraper.Api.Scapers.Entities;

namespace LunchTime.Main.MenuConverters.Converters
{
    public class ImageMenuConverter : IMenuConverter
    {
        private readonly IImageToTextQueryHandler _imageToText;

        public ImageMenuConverter(IImageToTextQueryHandler imageToText)
        {
            _imageToText = imageToText;
        }

        public async Task<RestaurantMenu> Convert(int restaurantId, ScrapeResult scrapeResult)
        {
            var resultText = await _imageToText.Execute(new ImageToTextQuery
            {
                Uri = new Uri(scrapeResult.Result)
            });

            return new RestaurantMenu
            {
                HtmlMenu = resultText,
                RestaurantId = restaurantId
            };
        }
    }
}