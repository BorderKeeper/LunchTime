using System;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LunchTime.Core.Windsor;
using LunchTime.Main;
using LunchTime.Main.Api.MenuConverters;
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
            new MainInstaller().Install(container, configuration);

            var scraper = ServiceLocator.Resolve<IScrapePageContentQueryHandler>();
            var converterFactory = ServiceLocator.Resolve<IMenuConverterFactory>();

            ScrapeResult result = scraper.Execute(new ScrapePageContentQuery
            {
                WebsiteUri = new Uri("http://www.veselacajovna.cz/tydenni-nabidka/"),
                Target = new NaiveScrapeTarget("//a[@class='pdfemb-viewer']"),
                ScrapeResultType = ScrapeResultType.Pdf
            }).Result;

            var converter = converterFactory.GetConverter(result.ResultyType);

            var menu = converter.Convert(1, result).Result;

            Console.WriteLine("Result: {0}", menu.HtmlMenu);

            Console.ReadKey();
        }
    }
}
