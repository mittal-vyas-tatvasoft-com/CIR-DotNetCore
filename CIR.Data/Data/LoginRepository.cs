using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.Users;
using CIR.Core.Interfaces;
using CIR.Core.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomStringCreator;
using System.Data;

namespace CIR.Data.Data
{
    public class LoginRepository : ILoginRepository
    {
        #region PROPERTIES
        private readonly EmailGeneration emailGeneration;
        private readonly JwtGenerateToken jwtGenerateToken;
        #endregion

        #region CONSTRUCTOR
        public LoginRepository(EmailGeneration _emailGeneration, JwtGenerateToken _jwtGenerateToken)
        {
            emailGeneration = _emailGeneration;
            jwtGenerateToken = _jwtGenerateToken;
        }
        #endregion

        #region METHODS

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="model">The login model.</param>
        /// <returns>Generate the Token if user is valid and if user's account is not locked.</returns>
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                User userResult;
                DbConnection dbConnection = new DbConnection();
                var result = 0;
                using (var connection = dbConnection.Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@userName", model.UserName);
                    parameters.Add("@password", model.Password);
                    userResult = await Task.FromResult(connection.Query<User>("spLogin", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault());

                    if (userResult != null)
                    {
                        if (userResult.ResetRequired)
                        {
                            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Forbidden, Result = false, Message = SystemMessages.msgAccountIsLocked });
                        }

                        else
                        {
                            var generateToken = await jwtGenerateToken.GenerateJwtToken(userResult);
                            if (generateToken != null)
                            {
                                connection.Execute("spResetLoginAttempts", parameters, commandType: CommandType.StoredProcedure);
                                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = generateToken });
                            }
                            else
                            {
                                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgTokenNotGenerated });
                            }
                        }
                    }
                    else
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@userName", model.UserName);
                        userResult = await Task.FromResult(connection.Query<User>("spGetUserDataForLogin", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault());

                        if (userResult != null)
                        {
                            if (userResult.LoginAttempts < 5)
                            {
                                parameters = new DynamicParameters();
                                parameters.Add("@userId", userResult.Id);
                                result = await Task.FromResult(connection.Execute("spIncrementLoginAttempts", parameters, commandType: CommandType.StoredProcedure));

                                if (result == 1)
                                {
                                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = string.Format(SystemMessages.msgInvalidUserNameOrPassword) });
                                }
                            }
                            else
                            {
                                parameters = new DynamicParameters();
                                parameters.Add("@userId", userResult.Id);
                                result = await Task.FromResult(connection.Execute("spResetRequired", parameters, commandType: CommandType.StoredProcedure));

                                if (result == 1)
                                {
                                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Forbidden, Result = false, Message = SystemMessages.msgAccountIsLocked });
                                }
                            }
                        }
                        else
                        {
                            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgInvalidUserNameOrPassword });
                        }
                    }
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgInvalidUserNameOrPassword });
                }
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="forgotPasswordModel">The forgot password model.</param>
        /// <returns>send the password to user's mail and reset the password from there.</returns>
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                User user;
                var result = 0;
                DbConnection dbConnection = new DbConnection();
                using (var connection = dbConnection.Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@userName", forgotPasswordModel.UserName);
                    user = await Task.FromResult(connection.Query<User>("spGetUserDataForLogin", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault());

                    if (user != null)
                    {
                        string randomString = SystemConfig.randomString;
                        string newPassword = new StringCreator(randomString).Get(8);

                        parameters = new DynamicParameters();
                        parameters.Add("@userName", forgotPasswordModel.UserName);
                        parameters.Add("@password", newPassword);
                        parameters.Add("@ResetRequired", true);
                        result = await Task.FromResult(connection.Execute("spResetPassword", parameters, commandType: CommandType.StoredProcedure));

                        if (result == 1)
                        {
                            string mailSubject = EmailGeneration.ForgotPasswordSubject();
                            string mailBody = EmailGeneration.ForgotPasswordTemplate(user);
                            emailGeneration.SendMail(forgotPasswordModel.UserName, mailSubject, mailBody);

                            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = SystemMessages.msgSendNewPasswordOnMail });
                        }
                    }
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = SystemMessages.msgEnterValidUserName });
                }
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns>reset the password if user enter valid password and email.</returns>
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                User user;
                var result = 0;
                DbConnection dbConnection = new DbConnection();

                using (var connection = dbConnection.Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@userName", resetPasswordModel.UserName);
                    user = await Task.FromResult(connection.Query<User>("spGetUserDataForLogin", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault());

                    if (user != null)
                    {
                        if (user.Password == resetPasswordModel.OldPassword)
                        {
                            parameters = new DynamicParameters();
                            parameters.Add("@UserName", resetPasswordModel.UserName);
                            parameters.Add("@Password", resetPasswordModel.NewPassword);
                            parameters.Add("@ResetRequired", false);
                            result = await Task.FromResult(connection.Execute("spResetPassword", parameters, commandType: CommandType.StoredProcedure));

                            if (result == 1)
                            {
                                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = SystemMessages.msgPasswordChangedSuccessfully });
                            }
                        }
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = SystemMessages.msgIncorrectOldPassword });
                    }
                    else
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = SystemMessages.msgInvalidEmailAddress });
                    }
                }
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }
        #endregion
    }
}
