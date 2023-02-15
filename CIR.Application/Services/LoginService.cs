using CIR.Core.Interfaces;
using CIR.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Application.Services
{
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository loginRepository;
        public LoginService(ILoginRepository _loginRepository)
        {
            loginRepository = _loginRepository;
        }
        public async Task<IActionResult> Login(LoginModel model)
        {
            return await loginRepository.Login(model);
        }
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            return await loginRepository.ForgotPassword(forgotPasswordModel);
        }
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            return await loginRepository.ResetPassword(resetPasswordModel);
        }
    }
}
