using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using NLog;
using NLog.Fluent;
using PredictionOfDelays.Core.Repositories;
using PredictionOfDelays.Infrastructure.Repositories;
using PredictionOfDelays.Infrastructure.Services;
using WebGrease.Css.Extensions;

namespace PredictionOfDelays.Api.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly IUserEventService _userEventService;

        public NotificationsHub(IUserEventService userEventService)
        {
            _userEventService = userEventService;
        }

        public async Task Hello()
        {
            var userId = Context.User.Identity.GetUserId();
            var events = await _userEventService.GetEventsAsync(userId);
            foreach (var @event in events)
            {
                Clients.OthersInGroup(@event.Name).hello(@event.Name);
            }
        }

        public void SendNotification(string groupName, string message)
        {
            
        }

        public override async Task OnConnected()
        {
            var userId = Context.User.Identity.GetUserId();
            await _userEventService.AddConnectionId(userId, Context.ConnectionId);
            var events = await _userEventService.GetEventsAsync(userId);
            foreach (var e in events)
            {
                await Groups.Add(Context.ConnectionId, e.Name);
            }

            await base.OnConnected();
        }

        public override async Task OnReconnected()
        {
            var userId = Context.User.Identity.GetUserId();
            await _userEventService.AddConnectionId(userId, Context.ConnectionId);
            var events = await _userEventService.GetEventsAsync(userId);
            foreach (var e in events)
            {
                await Groups.Add(Context.ConnectionId, e.Name);
            }

            await base.OnReconnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var userId = Context.User.Identity.GetUserId();
            await _userEventService.RemoveConnectionId(userId, Context.ConnectionId);
            var events = await _userEventService.GetEventsAsync(userId);
            foreach (var e in events)
            {
                await Groups.Remove(Context.ConnectionId, e.Name);
            }

            await base.OnDisconnected(true);
        }
    }
}