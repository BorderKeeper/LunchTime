using System;
using HtmlAgilityPack;

namespace LunchTime.Scraper.Api.Scapers.Entities
{
    public class CachedWebsite
    {
        public string AbsoluteUri { get; set; }

        public HtmlDocument Document { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}