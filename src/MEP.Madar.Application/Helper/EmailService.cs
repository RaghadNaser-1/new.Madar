using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace MEP.Madar.Helper
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            SmtpSettings smtpSettings = new()
            {
                Username = _configuration["SmtpSettings:Username"],
                Password = _configuration["SmtpSettings:Password"],
            };
            var val = _configuration.GetSection("SmtpSettings");
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
            };

            //var smtpClient = new SmtpClient(smtpSettings.Server, smtpSettings.Port)
            //{
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
            //    EnableSsl = true // Ensure SSL is enabled
            //};

            var message = new MailMessage(smtpSettings.Username, to, subject, body);

            await smtp.SendMailAsync(message);
        }
    }
}