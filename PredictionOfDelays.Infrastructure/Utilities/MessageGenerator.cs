using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;
using RazorEngine;
using RazorEngine.Templating;

namespace PredictionOfDelays.Infrastructure.Utilities
{
    public class MessageGenerator
    {
        private const string EventInviteViewPath =
            @"C:\Users\Bartlomiej\dev\BackEnd\PredictionOfDelays.Infrastructure\Utilities\EventInvite.cshtml";
        private const string EventInviteTitle = "New event invite!";
        public MailMessage GenerateEventInviteMessage(EventInvite invite)
        {
            var message = new MailMessage();
            message.To.Add(invite.Invited.Email);
            // message.From = new MailAddress(ConfigurationManager.AppSettings["email-address"]);
            message.Subject = EventInviteTitle;
            message.IsBodyHtml = true;
            var template =
                File.ReadAllText(EventInviteViewPath);
            var view = Engine.Razor.RunCompile(template,"templateKey", typeof(EventInvite), invite);
            message.Body = view;

            return message;
        }
    }
}
