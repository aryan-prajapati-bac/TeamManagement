﻿using System.Net.Mail;
using System.Net;
using TeamManagement.Interfaces;

namespace TeamDemo.Services
{
    public class MailService : IMailServices
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public MailService(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public void SendEmail(string to, string subject, string body)
        {
            string smtpServer = _configuration["EmailSettings:SmtpServer"];
            int smtoPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            string smtpUsername = _configuration["EmailSettings:SmtpUsername"];
            string smtpPwd = _configuration["EmailSettings:SmtpPassword"];
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(smtpUsername);
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                try
                {
                    using (var client = new SmtpClient(smtpServer, smtoPort))
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(smtpUsername, smtpPwd);
                        client.EnableSsl = true;

                        client.Send(message);
                    }
                }
                catch(Exception ex) {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.StackTrace); }
            }
        }
    }
}