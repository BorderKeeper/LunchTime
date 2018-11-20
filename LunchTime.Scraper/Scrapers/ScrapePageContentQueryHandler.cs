using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using LunchTime.Core.Api.Common.Enums;
using LunchTime.Core.Extensions;
using LunchTime.Scraper.Api.Scapers;
using LunchTime.Scraper.Api.Scapers.Entities;
using LunchTime.Scraper.Api.Scapers.Queries;

namespace LunchTime.Scraper.Scrapers
{
    public class ScrapePageContentQueryHandler : IScrapePageContentQueryHandler
    {
        private static readonly List<Extensions> ValidExtensions =
            new List<Extensions> {Extensions.Jpeg, Extensions.Jpg, Extensions.Pdf, Extensions.Png};

        private readonly IWebsiteContentQueryHandler _websiteContentQueryHandler;

        public ScrapePageContentQueryHandler(IWebsiteContentQueryHandler websiteContentQueryHandler)
        {
            _websiteContentQueryHandler = websiteContentQueryHandler;
        }

        public async Task<ScrapeResult> Execute(ScrapePageContentQuery query)
        {
            var document = _websiteContentQueryHandler.Execute(new WebsiteContentQuery
            {
                Uri = query.WebsiteUri
            });

            var scrapedNodes = query.Target.Scrape(await document);

            if (query.ScrapeResultType == ScrapeResultType.Text)
            {
                return ExtractHtmlMenu(query, scrapedNodes);
            }

            return ExtractLink(query, scrapedNodes);
        }

        private ScrapeResult ExtractLink(ScrapePageContentQuery query, IEnumerable<HtmlNode> scrapedNodes)
        {
            var node = scrapedNodes.First();

            foreach(var attribute in node.Attributes)
            {
                var attributeExtension = attribute.Value.Split('.').Last();

                if (ValidExtensions.Any(extension => extension.ToString().EqualsIgnoreCase(attributeExtension)))
                {
                    return new ScrapeResult { Result = attribute.Value, ResultyType = query.ScrapeResultType };
                }
            }

            throw new InvalidOperationException("Restaurant menu node does not have a link associated with it.");
        }

        private ScrapeResult ExtractHtmlMenu(ScrapePageContentQuery query, IEnumerable<HtmlNode> scrapedNodes)
        {
            var rawHtml = ConvertNodesToString(scrapedNodes);

            return new ScrapeResult { Result = rawHtml, ResultyType = query.ScrapeResultType };
        }

        private string ConvertNodesToString(IEnumerable<HtmlNode> scrapedNodes)
        {
            //TODO: convert nodes to string
            var rawHtml = new StringBuilder();

            foreach (HtmlNode node in scrapedNodes)
            {
                rawHtml.Append(node.InnerHtml);
            }

            return rawHtml.ToString();
        }
    }
}