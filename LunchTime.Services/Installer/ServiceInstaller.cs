using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LunchTime.Core.Windsor;

namespace LunchTime.Services.Installer
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestylePerWebRequest());

            new RootInstaller().Install(container, store);
        }
    }
}