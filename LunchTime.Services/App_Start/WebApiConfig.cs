using System.Web.Http;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LunchTime.Services.Installer;

namespace LunchTime.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //IoC Installers
            var container = new WindsorContainer();
            var store = new DefaultConfigurationStore();
            new ServiceInstaller().Install(container, store);
            config.DependencyResolver = new DependencyResolver(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
