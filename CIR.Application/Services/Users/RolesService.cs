using CIR.Core.Interfaces.Users;
using CIR.Core.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Application.Services.Users
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository rolesRepository;
        public RolesService(IRolesRepository _rolesRepository)
        {
            this.rolesRepository = _rolesRepository;
        }

        public async Task<IActionResult> GetAllRoles()
        {
            return await rolesRepository.GetAllRoles();
        }
        public async Task<IActionResult> GetRoles(int displayLength, int displayStart, string? sortCol, string? search, bool sortAscending = true)
        {
            return await rolesRepository.GetRoles(displayLength, displayStart, sortCol, search, sortAscending);
        }
        public async Task<IActionResult> GetLanguagesListByRole()
        {
            return await rolesRepository.GetLanguagesListByRole();
        }
        public async Task<IActionResult> GetRolePrivilegesList()
        {
            return await rolesRepository.GetRolePrivilegesList();
        }

        public async Task<IActionResult> GetSubSiteList()
        {
            return await rolesRepository.GetSubSiteList();
        }
        public async Task<Boolean> RoleExists(string roleName, long id)
        {
            return await rolesRepository.RoleExists(roleName, id);
        }
        public async Task<IActionResult> CreateOrUpdateRole(RolePermissionModel roles)
        {
            return await rolesRepository.CreateOrUpdateRole(roles);
        }
        public async Task<IActionResult> GetRoleDetailById(long roleId)
        {
            return await rolesRepository.GetRoleDetailById(roleId);
        }
        public async Task<IActionResult> DeleteRoles(long roleId)
        {
            return await rolesRepository.DeleteRoles(roleId);
        }

        public async Task<IActionResult> RemoveSection(long groupId)
        {
            return await rolesRepository.RemoveSection(groupId);
        }
    }
}
