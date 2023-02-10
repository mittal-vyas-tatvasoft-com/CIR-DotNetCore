using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.Users;
using CIR.Core.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.Users
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		#region Properties
		private readonly IUserService userService;
		private readonly ILogger<UsersController> loggerr;
		#endregion

		#region Construction
		public UsersController(IUserService service, ILogger<UsersController> logger)
		{
			userService = service;
			loggerr = logger;
		}
		#endregion

		#region Methods
		/// <summary>
		/// This method fetches single user data using user's Id
		/// </summary>
		/// <param name="id">user will be fetched according to this 'id'</param>
		/// <returns> user </returns> 

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			try
			{
				return await userService.GetUserById(id);
			}
			catch (Exception ex)
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
			}
		}

		/// <summary>
		/// This method takes user details as parameters and creates user and returns that user
		/// </summary>
		/// <param name="user"> this object contains different parameters as details of a user </param>
		/// <returns > created user </returns>
		[HttpPost("[action]")]
		public async Task<IActionResult> Create([FromBody] User user)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var userExists = await userService.UserExists(user.Email, user.Id);
					if (userExists)
					{
						return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = string.Format(SystemMessages.msgDataExists, "User") });
					}
					else
					{
						return await userService.CreateOrUpdateUser(user);

					}

				}
				catch (Exception ex)
				{
					return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
				}
			}
			return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
		}

		/// <summary>
		/// This method takes user details and updates the user
		/// </summary>
		/// <param name="user"> this object contains different parameters as details of a user </param>
		/// <returns> updated user </returns>
		[HttpPut("[action]")]
		public async Task<IActionResult> Update([FromBody] User user)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var userExists = await userService.UserExists(user.Email, user.Id);
					if (userExists)
					{
						return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
					}
					else
					{
						return await userService.CreateOrUpdateUser(user);
					}
				}
				catch (Exception ex)
				{
					return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
				}
			}
			return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
		}
		/// <summary>
		/// This method disables user 
		/// </summary>
		/// <param name="id"> user will be disabled according to this id </param>
		/// <returns> disabled user </returns>
		[HttpDelete("[action]")]
		public async Task<IActionResult> Delete(int id)
		{
			if (ModelState.IsValid)
			{
				try
				{
					return await userService.DeleteUser(id);
				}
				catch (Exception ex)
				{
					return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
				}
			}
			return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
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
		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllUsersDetailBySP(int displayLength, int displayStart, string? sortCol, string? search, string? sortDir, int roleId, bool? enabled = null)
		{
			try
			{
				search ??= string.Empty;
				return await userService.GetAllUsersDetailBySP(displayLength, displayStart, sortCol, search, sortDir, roleId, enabled);
			}
			catch (Exception ex)
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
			}
		}
		#endregion
	}
}
