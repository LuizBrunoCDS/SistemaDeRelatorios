using Domain.Interfaces;
using Infra.Context;
using Infra.Repositories;
using Unity;
using Unity.Lifetime;

namespace DependencyInjection.Injection
{
    public static class Injection
    {
        public static UnityContainer RegisterInjection()
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new SingletonLifetimeManager());
            container.RegisterType<RelatoriosContext>(new SingletonLifetimeManager());
            return container;
        }
    }
}
