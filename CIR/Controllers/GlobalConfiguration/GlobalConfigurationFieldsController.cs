using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class GlobalConfigurationFieldsController : ControllerBase
	{
		#region PROPERTIES
		private readonly IGlobalConfigurationFieldsService globalConfigurationFieldsService;

		#endregion
		#region CONSTRUCTOR
		public GlobalConfigurationFieldsController(IGlobalConfigurationFieldsService iGlobalConfigurationFieldsServices)
		{
			globalConfigurationFieldsService = iGlobalConfigurationFieldsServices;
		}
		#endregion

		#region METHODS
		/// <summary>
		/// This method returns list of global configuration fields 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAllGlobalConfigurationFields()
		{
			try
			{
				return await globalConfigurationFieldsService.GetAllGlobalConfigurationFields();
			}
			catch (Exception ex)
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
			}
		}

		/// <summary>
		/// This method takes add global configuration fields
		/// </summary>
		/// <param name="globalConfigurationFields">this object contains different parameters as details of a globalFields</param>
		/// <returns>Success status if input is valid else failure status</returns>
		[HttpPost("[action]")]
		public async Task<IActionResult> Create(List<GlobalConfigurationField> globalConfigurationFields)
		{
			if (ModelState.IsValid)
			{
				try
				{
					return await globalConfigurationFieldsService.CreateOrUpdateGlobalConfigurationFields(globalConfigurationFields);
				}
				catch (Exception ex)
				{
					return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
				}
			}
			return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
		}

		/// <summary>
		/// This method takes update configuration fields
		/// </summary>
		/// <param name="globalConfigurationFields">this object contains different parameters as details of a globalFields</param>
		/// <returns>Success status if input is valid else failure status</returns>
		[HttpPut("[action]")]
		public async Task<IActionResult> Update(List<GlobalConfigurationField> globalConfigurationFields)
		{
			if (ModelState.IsValid)
			{
				try
				{
					return await globalConfigurationFieldsService.CreateOrUpdateGlobalConfigurationFields(globalConfigurationFields);
				}
				catch (Exception ex)
				{
					return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
				}
			}
			return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
		}
		#endregion
	}
}
