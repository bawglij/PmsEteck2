using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PmsEteck.Helpers
{
    public class sendEmail : IEmailSender
    {
        public static string MailAddressFrom = "jasmine@refine-it.nl";
        //= ConfigurationManager.AppSettings["mailAddressFrom"];
        public static string Host = "smtp.gmail.com";
        public static string Port = "587";
        public static bool HasCredentials = true;

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(MailAddressFrom, "PMS Beheerder");
            msg.To.Add(new MailAddress(email));
            string template;
            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "EmailTemplate.txt"), Encoding.UTF8))
            //new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ), Encoding.UTF8))
            {
                template = reader.ReadToEnd();
            }
            msg.Body = template.Replace(" - subject -", subject).Replace("- body -", htmlMessage);
            msg.Subject = subject;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("vboglis@gmail.com", "Boglisdariana23?");
            smtpClient.TargetName = "STARTTLS/smtp.gmail.com";

            smtpClient.Send(msg);

            return Task.FromResult(0);
        }
    }
    
}
