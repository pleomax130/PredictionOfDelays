using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Microsoft.AspNet.SignalR.Hubs;

namespace PredictionOfDelays.Api.Hubs
{
    public class HubActivator : IHubActivator
    {
        private readonly IContainer _container;

        public HubActivator(IContainer container)
        {
            _container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)_container.Resolve(descriptor.HubType);
        }
    }
}