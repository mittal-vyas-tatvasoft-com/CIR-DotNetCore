using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Interfaces;
using CIR.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CIR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region PROPERTIES
        private readonly ILoginService loginService;
        private readonly AppSettings appSettings;
        #endregion

        #region CONSTUCTOR
        public LoginController(ILoginService _loginService, IOptions<AppSettings> _settings)
        {
            loginService= _loginService;
            appSettings=_settings.Value;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Logins the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Generate Token if UserName and Password are valid</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel value)
        {
            if (ModelState.IsValid)
            {
                return await loginService.Login(value);
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="forgotPasswordModel">The forgot password model.</param>
        /// <returns>Send mail to user if UserName/Email is valid</returns>
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await loginService.ForgotPassword(forgotPasswordModel);
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns>Reset the password when User enters valid Password</returns>
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await loginService.ResetPassword(resetPasswordModel);
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }
        #endregion
    }
}
