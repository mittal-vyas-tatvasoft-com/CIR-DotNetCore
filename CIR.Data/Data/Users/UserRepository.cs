using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Common.Helper.EmailTemplates;
using CIR.Core.Entities.Users;
using CIR.Core.Interfaces.Users;
using CIR.Core.ViewModel.Users;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CIR.Data.Data.Users
{
	public class UserRepository : IUserRepository
	{
		#region Properties
		private readonly CIRDbContext cIRDbContext;
		private readonly EmailGeneration emailGeneration;
		#endregion

		#region Constructor
		public UserRepository(CIRDbContext context, EmailGeneration emailService)
		{
			cIRDbContext = context ?? throw new ArgumentNullException(nameof(context));
			emailGeneration = emailService;
		}
		#endregion

		#region Methods
		/// <summary>
		/// fetches user based on input user id
		/// </summary>
		/// <param name="id"></param>
		/// <returns> user or null user if not found </returns>
		public async Task<IActionResult> GetUserById(int id)
		{
			try
			{
				List<User> usersList;
				using (DbConnection dbConnection = new DbConnection())
				{
					using (var connection = dbConnection.Connection)
					{
						DynamicParameters parameters = new DynamicParameters();
						parameters.Add("id", id);
						usersList = connection.Query<User>("spGetUserIdWise", parameters, commandType: CommandType.StoredProcedure).ToList();
					}
				}
				if (usersList.Count == 0)
				{
					return new JsonResult(new CustomResponse<User>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Users") });
				}
				return new JsonResult(new CustomResponse<List<User>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = usersList });
			}
			catch (Exception ex)
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
			}
		}

		/// <summary>
		/// this meethod checks if user exists or not based on input user email
		/// </summary>
		/// <param name="email"></param>
		/// <param name="id"></param>
		/// <returns> if user exists true else false </returns>
		public async Task<Boolean> UserExists(string email, long id)
		{
			var result = false;
			using (DbConnection dbConnection = new DbConnection())
			{
				using (var connection = dbConnection.Connection)
				{
					DynamicParameters parameters = new DynamicParameters();
					parameters.Add("@email", email);
					parameters.Add("@id", id);
					result = Convert.ToBoolean(connection.ExecuteScalar("spUserExists", parameters, commandType: CommandType.StoredProcedure));
				}
			}
			return result;
		}

		/// <summary>
		/// This method is used by create method and update method of user controller
		/// </summary>
		/// <param name="user"> new user data or update data for user </param>
		/// <returns> Ok status if its valid else unprocessable </returns>

		public async Task<IActionResult> CreateOrUpdateUser(User user)
		{
			try
			{
				int result;
				using (DbConnection dbConnection = new DbConnection())
				{
					using (var connection = dbConnection.Connection)
					{
						DynamicParameters parameters = new DynamicParameters();
						parameters.Add("@Id", user.Id);
						parameters.Add("@UserName", user.UserName);
						parameters.Add("@Password", user.Password);
						parameters.Add("@Email", user.Email);
						parameters.Add("@SalutationLookupItemId", user.SalutationLookupItemId);
						parameters.Add("@FirstName", user.FirstName);
						parameters.Add("@LastName", user.LastName);
						parameters.Add("@RoleId", user.RoleId);
						parameters.Add("@Enabled", user.Enabled);
						parameters.Add("@ResetRequired", user.ResetRequired);
						parameters.Add("@DefaultAdminUser", user.DefaultAdminUser);
						parameters.Add("@TimeZone", user.TimeZone);
						parameters.Add("@CultureLcid", user.CultureLcid);
						parameters.Add("@EmployeeId", user.EmployeeId);
						parameters.Add("@PhoneNumber", user.PhoneNumber);
						parameters.Add("@ScheduledActiveChange", user.ScheduledActiveChange);
						parameters.Add("@LoginAttempts", user.LoginAttempts);
						parameters.Add("@CompanyName", user.CompanyName);
						parameters.Add("@PortalThemeId", user.PortalThemeId);
						result = await Task.FromResult(connection.Execute("spAddUpdateUsers", parameters, commandType: CommandType.StoredProcedure));
					}
				}
				if (result > 0 && user.Id == 0)
				{
					//send user creation mail 
					string mailSubject = AccountCreationTemplates.AccountCreationSubject();
					string mailbody = AccountCreationTemplates.UserAccountCreationTemplate(user);

					emailGeneration.SendMail(user.Email, mailSubject, mailbody);
					return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Users") });
				}
				if (result > 0 && user.Id != 0)
				{
					return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataUpdatedSuccessfully, "Users") });
				}
				return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
			}
			catch (Exception ex)
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
			}
		}

		/// <summary>
		/// this metohd updates a column value and disables user
		/// </summary>
		/// <param name="id"></param>
		/// <returns> deleted/disabled user </returns>
		public async Task<IActionResult> DeleteUser(int id)
		{
			try
			{
				if (id == 0)
				{
					return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgInvalidId, "Users") });
				}
				var result = 0;
				using (DbConnection dbConnection = new DbConnection())
				{
					using (var connection = dbConnection.Connection)
					{
						DynamicParameters parameters = new DynamicParameters();
						parameters.Add("Id", id);
						result = connection.Execute("spDeleteUser", parameters, commandType: CommandType.StoredProcedure);
					}
				}
				if (result != 0)
				{
					return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataDeletedSuccessfully, "Users") });
				}
				return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
			}
			catch
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
			}
		}

		/// <summary>
		/// This method retuns filtered user list using Store Procedure
		/// </summary>
		/// <param name="displayLength">how many row/data we want to fetch(for pagination)</param>
		/// <param name="displayStart">from which row we want to fetch(for pagination)</param>
		/// <param name="sortCol"> name of column which we want to sort</param>
		/// <param name="search">word that we want to search in user table</param>
		/// <param name="sortDir">'asc' or 'desc' direction for sort </param>
		/// <param name="roleId">sorting roleid wise</param>
		/// <param name="enabled">sorting enable wise</param>
		/// <returns></returns>
		public async Task<IActionResult> GetAllUsersDetailBySP(int displayLength, int displayStart, string? sortCol, string? search, string? sortDir, int roleId, bool? enabled = null)
		{
			UsersViewModel users = new();

			if (string.IsNullOrEmpty(sortCol))
			{
				sortCol = "Id";
			}
			try
			{
				List<UserModel> sortData;
				using (DbConnection dbConnection = new DbConnection())
				{
					using (var connection = dbConnection.Connection)
					{
						DynamicParameters parameters = new DynamicParameters();
						parameters.Add("@DisplayLength", displayLength);
						parameters.Add("@DisplayStart", displayStart);
						parameters.Add("@SortCol", sortCol);
						parameters.Add("@Search", search);
						parameters.Add("@SortDir", sortDir);
						parameters.Add("@RoleId", roleId);
						parameters.Add("@Enabled", enabled);
						sortData = connection.Query<UserModel>("spGetFilteredUsersList", parameters, commandType: CommandType.StoredProcedure).ToList();
					}
				}
				if (sortData.Count > 0)
				{
					sortData = sortData.ToList();
					users.Count = sortData[0].TotalCount;
					users.UsersList = sortData;
				}
				else
				{
					users.Count = 0;
					users.UsersList = sortData;
				}
				return new JsonResult(new CustomResponse<UsersViewModel>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = users });
			}
			catch (Exception ex)
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
			}
		}
		#endregion
	}
}
