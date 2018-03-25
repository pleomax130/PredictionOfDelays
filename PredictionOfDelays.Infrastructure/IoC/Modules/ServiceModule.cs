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
            builder.RegisterType<EventService>()
                .As<IEventService>()
                .SingleInstance();
            builder.RegisterType<GroupService>()
                .As<IGroupService>()
                .SingleInstance();
            builder.RegisterType<UserEventService>()
                .As<IUserEventService>()
                .SingleInstance();
            builder.RegisterType<UserGroupService>()
                .As<IUserGroupService>()
                .SingleInstance();
        }
    }
}