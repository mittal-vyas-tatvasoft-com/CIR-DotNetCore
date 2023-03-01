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

        public static string GenerateRandomString()
        {
            var capitalLatter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var number = "0123456789";
            var smallLatter = "abcdefghijklmnopqrstuvwxyz";
            var specialCharacter = "!@$%&?*";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < 2; i++)
            {
                stringChars[i] = capitalLatter[random.Next(capitalLatter.Length)];
            }
            for (int i = 2; i < 3; i++)
            {
                stringChars[i] = specialCharacter[random.Next(specialCharacter.Length)];
            }
            for (int i = 3; i < 7; i++)
            {
                stringChars[i] = smallLatter[random.Next(smallLatter.Length)];
            }
            for (int i = 7; i < 8; i++)
            {
                stringChars[i] = number[random.Next(number.Length)];
            }

            var randomString = new String(stringChars);
            return randomString;
        }

    }
}
