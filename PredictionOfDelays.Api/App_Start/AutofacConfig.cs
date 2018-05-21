using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PredictionOfDelays.Api.Hubs;
using PredictionOfDelays.Infrastructure.IoC.Modules;
using PredictionOfDelays.Infrastructure.Mappers;
using PredictionOfDelays.Infrastructure.Services;

namespace PredictionOfDelays.Api
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterModules(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }

        private static IContainer RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationsHub>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();
            Container = builder.Build();
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(Container));
            return Container;
        }
    }
}