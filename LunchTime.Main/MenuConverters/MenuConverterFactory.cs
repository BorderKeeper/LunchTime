using System;
using LunchTime.Main.Api.ImageRecognition;
using LunchTime.Main.Api.MenuConverters;
using LunchTime.Main.MenuConverters.Converters;
using LunchTime.Scraper.Api.Scapers.Entities;

namespace LunchTime.Main.MenuConverters
{
    public class MenuConverterFactory : IMenuConverterFactory
    {
        private readonly IImageToTextQueryHandler _imageToTextQueryHandler;

        public MenuConverterFactory(IImageToTextQueryHandler imageToTextQueryHandler)
        {
            _imageToTextQueryHandler = imageToTextQueryHandler;
        }

        public IMenuConverter GetConverter(ScrapeResultType scrapeResultType)
        {
            switch (scrapeResultType)
            {
                case ScrapeResultType.Text:
                    return new TextMenuConverter();
                case ScrapeResultType.Image:
                    return new ImageMenuConverter(_imageToTextQueryHandler);
                case ScrapeResultType.Pdf:
                    return new PdfMenuConverter();
            }

            throw new NotSupportedException("This result type conversion is not implemented.");
        }
    }
}