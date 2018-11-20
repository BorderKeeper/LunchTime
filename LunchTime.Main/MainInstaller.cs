using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LunchTime.Main.Api.MenuConverters;
using LunchTime.Main.MenuConverters;

namespace LunchTime.Main
{
    public class MainInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMenuConverterFactory>()
                .ImplementedBy(typeof(MenuConverterFactory))
                .LifestyleTransient());
        }
    }
}