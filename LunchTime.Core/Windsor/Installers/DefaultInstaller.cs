using System;
using System.IO;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LunchTime.Core.Api;
using LunchTime.Core.Api.Common;

namespace LunchTime.Core.Windsor.Installers
{
    public class DefaultInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AssemblyDirectory))
                .BasedOn(typeof(ISingletonQueryHandler<,>)).WithService.FirstInterface().LifestyleSingleton()
            );

            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AssemblyDirectory))
                .BasedOn(typeof(IQueryHandler<,>)).WithService.FirstInterface().LifestyleTransient()
            );

            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AssemblyDirectory))
                .BasedOn(typeof(ICommandHandler<>)).WithService.FirstInterface().LifestyleTransient()
            );

            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AssemblyDirectory))
                .BasedOn(typeof(ICommandHandler<,>)).WithService.FirstInterface().LifestyleTransient()
            );
        }

        private string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;

                var uri = new UriBuilder(codeBase);

                var path = Uri.UnescapeDataString(uri.Path);

                return Path.GetDirectoryName(path);
            }
        }
    }
}