using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Infrastructure.Utilities
{
    public class InviteSender
    {
        private readonly MailAddress _mailAddress = new MailAddress("plokarzbartlomiej@gmail.com");
        private readonly MessageGenerator _messageGenerator = new MessageGenerator();
        public void SendEventInvite(EventInvite invite)
        {
            var message = _messageGenerator.GenerateEventInviteMessage(invite);

            using (var smtp = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings["email-address"],
                    Password = ConfigurationManager.AppSettings["email-password"]
                };
                smtp.Credentials = credentials;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }

        }
    }
}
