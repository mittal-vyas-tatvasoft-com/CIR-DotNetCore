using CIR.Core.Entities.Users;
using CIR.Core.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Application.Services.Users
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;
		public UserService(IUserRepository userepo)
		{
			userRepository = userepo;
		}
		public async Task<IActionResult> GetUserById(int id)
		{
			return await userRepository.GetUserById(id);
		}
		public async Task<Boolean> UserExists(string email, long id)
		{
			return await userRepository.UserExists(email, id);
		}
		public Task<IActionResult> CreateOrUpdateUser(User user)
		{
			return userRepository.CreateOrUpdateUser(user);
		}
		public async Task<IActionResult> DeleteUser(int id)
		{
			return await userRepository.DeleteUser(id);
		}
		public Task<IActionResult> GetAllUsersDetailBySP(int displayLength, int displayStart, string? sortCol, string? sortDir, string? search, int roleId, bool? enabled = null)
		{
			return userRepository.GetAllUsersDetailBySP(displayLength, displayStart, sortCol, sortDir, search, roleId, enabled);
		}
	}
}
