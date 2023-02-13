using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Interfaces.Users;
using CIR.Core.ViewModel.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        #region Property
        private readonly IRolesService rolesService;

        #endregion

        #region Constructor
        public RolesController(IRolesService _rolesService)
        {
            this.rolesService = _rolesService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// This method takes a get roles list
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                return await rolesService.GetAllRoles();
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method retuns filtered roles list using SP
        /// </summary>
        /// <param name="displayLength"> how many row/data we want to fetch(for pagination) </param>
        /// <param name="displayStart"> from which row we want to fetch(for pagination) </param>
        /// <param name="sortCol"> name of column which we want to sort</param>
        /// <param name="search"> word that we want to search in user table </param>
        /// <param name="sortAscending"> 'asc' or 'desc' direction for sort </param>
        /// <returns> filtered list of roles </returns>
        [HttpGet]
        public async Task<IActionResult> GetRoles(int displayLength, int displayStart, string? sortCol, string? search, bool sortAscending = true)
        {
            try
            {
                search ??= string.Empty;

                return await rolesService.GetRoles(displayLength, displayStart, sortCol, search, sortAscending);
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method return role detail of given id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> Get(int roleId)
        {
            try
            {
                return await rolesService.GetRoleDetailById(roleId);
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method used by get languages list by role
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLanguagesListByRole()
        {
            try
            {
                return await rolesService.GetLanguagesListByRole();
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method used by get role privileges list
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRolePrivilegesList()
        {
            try
            {
                return await rolesService.GetRolePrivilegesList();
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method used by get subsite list
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubSiteList()
        {
            try
            {
                return await rolesService.GetSubSiteList();
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes add role
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(RolePermissionModel roles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var isExist = await rolesService.RoleExists(roles.Name, roles.Id);
                    if (isExist)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = string.Format(SystemMessages.msgDataExists, "RoleName") });
                    }
                    else
                    {
                        return await rolesService.CreateOrUpdateRole(roles);
                    }
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method takes update role
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(RolePermissionModel roles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var isExist = await rolesService.RoleExists(roles.Name, roles.Id);
                    if (isExist)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = string.Format(SystemMessages.msgDataExists, "RoleName") });
                    }
                    else
                    {
                        return await rolesService.CreateOrUpdateRole(roles);
                    }
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method takes delete role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]

        public async Task<IActionResult> Delete(long roleId)
        {
            try
            {
                if (roleId > 0)
                {
                    return await rolesService.DeleteRoles(roleId);
                }
                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = SystemMessages.msgInvalidId });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes remove role id wise section
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpDelete("RemoveSection/{groupId}")]
        public async Task<IActionResult> RemoveSection(long groupId)
        {
            try
            {
                if (groupId > 0)
                {
                    return await rolesService.RemoveSection(groupId);
                }
                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = SystemMessages.msgInvalidId });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }
        #endregion
    }
}
