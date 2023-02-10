using CIR.Common.EmailGeneration;
using CIR.Core.Entities.Users;
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
		public static string AccountCreationSubject()
		{
			return "Account Created";
		}
		public static string UserAccountCreationTemplate(User user)
		{
			string template = "<div style=\"background-color:#800020;height:130px;font-family:arial;\">\r\n<br/><center><h1 style=\"color:White\">" + "Welcome" + "</h1></center>\t\t\r\n</div>\r\n<br/>\r\n<div style=\"font-family:arial;font-size:20px;font\" >\r\n"
							 + "Hello," + user.UserName + " Thank you for joining us.\r\n<br/>" +
							  "<table style=\"table-border:1px;padding:5px;border-spacing:10px;\">\r\n" +
							  "<tr><td>" + "Your Details are " + "</td></tr>\r\n" +
							  "<tr><td>" + "UserName:" + "</td><td>" + user.UserName + "</td></tr>\r\n" +
							  "<tr><td>" + "Password:" + "</td><td>" + user.Password + "</td></tr>\r\n" +
							  "<tr><td>" + "Email: " + "</td><td>" + user.Email + "</td></tr>\r\n" +
							  "<tr><td>" + "MobileNumber:</td><td>" + user.PhoneNumber + "</td></tr>\r\n</table>";
			return template;
		}
	}
}
