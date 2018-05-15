using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using NLog;
using NLog.Fluent;
using PredictionOfDelays.Infrastructure.Repositories;

namespace PredictionOfDelays.Api.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly UserEventService _userEventService = new UserEventService();
        public void Hello()
        {
            Clients.OthersInGroup("grupa").hello("hello");
        }

        public void SendNotification(string groupName, string message)
        {
            
        }

        public override async Task OnConnected()
        {
            await base.OnConnected();
//            var userId = Context.User.Identity.GetUserId();
            await Groups.Add(Context.ConnectionId, "grupa");
//            var events = await _userEventService.GetEvents(userId).Entity.ToListAsync();
//            foreach (var e in events)
//            {
//                await Groups.Add(Context.ConnectionId, e.Name);
//            }
        }

        public override async Task OnReconnected()
        {
            await base.OnReconnected();
//            var userId = Context.User.Identity.GetUserId();
            await Groups.Add(Context.ConnectionId, "grupa");
            //            var events = await _userEventService.GetEvents(userId).Entity.ToListAsync();
            //            foreach (var e in events)
            //            {
            //                await Groups.Add(Context.ConnectionId, e.Name);
            //            }
        }

        [Authorize]
        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(true);
//            var userId = Context.User.Identity.GetUserId();
//            var events = await _userEventService.GetEvents(userId).Entity.ToListAsync();
//            foreach (var e in events)
//            {
//                await Groups.Remove(Context.ConnectionId, e.Name);
//            }
        }
    }
}