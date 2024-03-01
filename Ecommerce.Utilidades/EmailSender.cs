using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilidades
{
    public class EmailSender : IEmailSender
    {

        public string SendGrid { get; set; }
        public EmailSender(IConfiguration _config)
        {
            SendGrid = _config.GetValue<string>("Key");
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var cliente = new SendGridClient(SendGrid);
            var from = new EmailAddress("email@hotmail.com");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            return cliente.SendEmailAsync(msg);
        }
    }
}
