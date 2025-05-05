using QuizReviewApplication.Application.Services;
using System.Net.Mail;
using System.Net;

namespace QuizReviewApplication.WebApi.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public SmtpEmailService(IConfiguration configuration)
        {
                _configuration = configuration;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSection = _configuration.GetSection("SmtpSettings");

            var smtpClient = new SmtpClient(smtpSection["Host"])
            {
                Port = int.Parse(smtpSection["Port"]),
                Credentials = new NetworkCredential(smtpSection["Username"], smtpSection["Password"]),
                EnableSsl = bool.Parse(smtpSection["EnableSsl"] ?? "true")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSection["SenderEmail"], smtpSection["SenderName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
