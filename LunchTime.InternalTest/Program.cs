using System;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LunchTime.Core.Windsor;
using LunchTime.Scraper.Api.Scapers;
using LunchTime.Scraper.Api.Scapers.Entities;
using LunchTime.Scraper.Api.Scapers.Entities.ScrapeTargets;
using LunchTime.Scraper.Api.Scapers.Queries;

namespace LunchTime.InternalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            var configuration = new DefaultConfigurationStore();
            new RootInstaller().Install(container, configuration);

            var scraper = ServiceLocator.Resolve<IScrapePageContentQueryHandler>();

            ScrapeResult result = scraper.Execute(new ScrapePageContentQuery
            {
                WebsiteUri = new Uri("http://www.zlatalod.com/denni-menu"),
                Target = new NaiveScrapeTarget("//section[@class='inBox menu-list']")
            }).Result;

            Console.WriteLine("Result: {0}", result.Result);

            Console.ReadKey();
        }
    }
}
