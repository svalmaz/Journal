using JournalAPI.Interfaces.Email;
using JournalAPI.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace JournalAPI.Services.Email
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _settings;
		public EmailService(IOptions<EmailSettings> settings)
		{
			_settings = settings.Value;
		}
		public void SendEmail(string recipientEmail, string subject, string body)
		{
			using var smtpClient = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
			{
				Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(_settings.SenderEmail),
				Subject = subject,
				Body =  body,
				IsBodyHtml = true
			};
			mailMessage.To.Add(recipientEmail);

			smtpClient.Send(mailMessage);
		}
	}
}
