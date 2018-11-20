using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace LunchTime.Services.Installer
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public DependencyResolver(IWindsorContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _scope = _container.BeginScope();
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>().ToArray();
        }

        public IDependencyScope BeginScope()
        {
            return new DependencyResolver(_container);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}