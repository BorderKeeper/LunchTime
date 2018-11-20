using System;
using Castle.Windsor;

namespace LunchTime.Core.Windsor
{
    public static class ServiceLocator
    {
        internal static IWindsorContainer Container { get; set; }

        public static TInterface Resolve<TInterface>()
        {
            return Container.Resolve<TInterface>();
        }
    }
}