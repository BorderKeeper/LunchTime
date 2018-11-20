using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LunchTime.Core.Windsor.Installers;

namespace LunchTime.Core.Windsor
{
    public class RootInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ServiceLocator.Container = container;

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            new DefaultInstaller().Install(container, store);
        }
    }
}