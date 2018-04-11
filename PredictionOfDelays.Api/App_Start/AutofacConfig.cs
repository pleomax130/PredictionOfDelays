using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
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
        }

        private static IContainer RegisterModules(ContainerBuilder builder)
        {

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();
            Container = builder.Build();

            return Container;
        }
    }
}