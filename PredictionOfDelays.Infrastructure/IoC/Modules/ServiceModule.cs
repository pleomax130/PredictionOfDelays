using Autofac;
using System.Reflection;
using PredictionOfDelays.Infrastructure.Services;
using Module = Autofac.Module;

namespace PredictionOfDelays.Infrastructure.IoC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo()
                .Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}