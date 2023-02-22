using CIR.Core.Entities.Users;

namespace CIR.Common.Helper.EmailTemplates
{
	public class ForgotPasswordTemplates
	{
		public static string ForgotPasswordTemplate(User user)
		{
			string template = "<p style=\"font-family:verdana;font-size:15px;\">" +
								"Hello " + user.UserName + ", <br/><br/> " +
								"Your New Login Password is : <b>" + user.Password + "</b>" +
							  "</p>";
			return template;
		}
	}
}
