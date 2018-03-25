using System.Reflection;
using Autofac;
using PredictionOfDelays.Core.Repositories;
using PredictionOfDelays.Infrastructure.Repositories;
using Module = Autofac.Module;

namespace PredictionOfDelays.Infrastructure.IoC.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule)
                .GetTypeInfo()
                .Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}