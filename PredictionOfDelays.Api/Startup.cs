using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PredictionOfDelays.Api.Hubs;
using PredictionOfDelays.Api.Providers;

[assembly: OwinStartup(typeof(PredictionOfDelays.Api.Startup))]

namespace PredictionOfDelays.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {

                map.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
                {
                    Provider = new QueryStringOAuthBearerProvider()
                });

                map.RunSignalR();
            });

            ConfigureAuth(app);            
        }
    }
}
