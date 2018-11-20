using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using LunchTime.Scraper.Api.Scapers.Entities;

namespace LunchTime.Scraper.Scrapers
{
    public class WebsiteContentQueryHandler : IWebsiteContentQueryHandler
    {
        private const int CachePeriodInMinutes = 15;

        private readonly List<CachedWebsite> _cachedPages;

        public WebsiteContentQueryHandler()
        {
            _cachedPages = new List<CachedWebsite>();
        }

        public Task<HtmlDocument> Execute(WebsiteContentQuery query)
        {
            Task<HtmlDocument> document = ShouldRetrieveFromCache(query.Uri)
                ? GetCacheContent(query.Uri)
                : GetWebsiteContent(query.Uri);

            return document;
        }

        private async Task<HtmlDocument> GetWebsiteContent(Uri uri)
        {            
            HtmlWeb web = new HtmlWeb();

            HtmlDocument document = await web.LoadFromWebAsync(uri.ToString());

            AddWebsiteToCache(uri, document);

            return document;
        }

        private Task<HtmlDocument> GetCacheContent(Uri uri)
        {
            var cachedPage = _cachedPages.Single(page => page.AbsoluteUri == uri.AbsoluteUri).Document;

            return Task.FromResult(cachedPage);
        }

        private void AddWebsiteToCache(Uri uri, HtmlDocument document)
        {
            if (!WebsiteIsCached(uri))
            {
                _cachedPages.Add(new CachedWebsite
                {
                    AbsoluteUri = uri.AbsoluteUri,
                    Document = document,
                    TimeStamp = DateTime.Now
                });
            }
        }

        private bool ShouldRetrieveFromCache(Uri uri)
        {
            if (WebsiteIsCached(uri))
            {
                var cachedWebsite = _cachedPages.Single(page => page.AbsoluteUri == uri.AbsoluteUri);

                return IsCachedWebsiteFresh(cachedWebsite.TimeStamp);
            }

            return false;
        }

        private bool IsCachedWebsiteFresh(DateTime timeStamp)
        {
            return (DateTime.Now - timeStamp).TotalMinutes < CachePeriodInMinutes;
        }

        private bool WebsiteIsCached(Uri uri)
        {
            return _cachedPages.Any(page => page.AbsoluteUri == uri.AbsoluteUri);
        }
    }
}