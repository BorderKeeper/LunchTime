using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LunchTime.Main.Api.MenuConverters;
using LunchTime.Main.Api.Retriever.Entities;
using LunchTime.Scraper.Api.Scapers.Entities;
using Path = System.IO.Path;

namespace LunchTime.Main.MenuConverters.Converters
{
    public class PdfMenuConverter : IMenuConverter
    {
        public async Task<RestaurantMenu> Convert(int restaurantId, ScrapeResult scrapeResult)
        {
            var tempFile = await StorePdfLocally(scrapeResult.Result);

            ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
            StringBuilder rawMenu = new StringBuilder();

            using (var reader = new PdfReader(tempFile))
            {
                for (int i = 0; i < reader.NumberOfPages; i++)
                {
                    rawMenu.Append(PdfTextExtractor.GetTextFromPage(reader, i + 1, strategy));
                }
            }

            rawMenu = rawMenu.Replace("\n", "<br>");

            return new RestaurantMenu
            {
                RestaurantId = restaurantId,
                HtmlMenu = rawMenu.ToString()
            };
        }

        private async Task<string> StorePdfLocally(string url)
        {
            var tempFile = Path.GetTempFileName();

            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(url), tempFile);
            }

            return tempFile;
        }
    }
}