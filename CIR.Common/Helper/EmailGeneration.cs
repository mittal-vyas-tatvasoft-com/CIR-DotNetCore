using CIR.Common.EmailGeneration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CIR.Common.Helper
{
	public class EmailGeneration
	{
		private readonly EmailModel emailModel;
		public EmailGeneration(IOptions<EmailModel> mailmodel)
		{
			emailModel = mailmodel.Value;

		}

		public void SendMail(string email, string mailSubject, string bodyTemplate)
		{
			try
			{
				string fromEmail = emailModel.FromEmail;
				MailMessage mailMessage = new MailMessage
				{
					From = new MailAddress(fromEmail)
				};
				mailMessage.To.Add(email);

				mailMessage.Subject = mailSubject;
				mailMessage.Body = bodyTemplate;
				mailMessage.IsBodyHtml = true;
				mailMessage.Priority = MailPriority.High;

				SmtpClient smtpClient = new SmtpClient
				{
					UseDefaultCredentials = false
				};

				NetworkCredential networkCredential = new NetworkCredential(fromEmail, emailModel.Password);
				smtpClient.Credentials = networkCredential;

				smtpClient.EnableSsl = emailModel.Enablessl;
				smtpClient.Port = emailModel.port;
				smtpClient.Host = emailModel.Host;
				smtpClient.Send(mailMessage);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public static string ForgotPasswordSubject()
		{
			return "Forgot Password";
		}

	}
}
