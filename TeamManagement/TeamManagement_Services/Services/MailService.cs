using System.Net.Mail;
using System.Net;
using TeamManagement.Interfaces;
using Microsoft.Extensions.Configuration;

namespace TeamDemo.Services
{
    public class MailService : IMailServices
    {
        #region Service
        private readonly IConfiguration _configuration;
        #endregion

        #region DI
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;            
        }
        #endregion

        #region Methods
        public async Task SendEmail(string to, string subject, string body)
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

                        await client.SendMailAsync(message);
                    }
                }
                catch(Exception ex) {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.StackTrace); }
            }
        }
        #endregion
    }
}
