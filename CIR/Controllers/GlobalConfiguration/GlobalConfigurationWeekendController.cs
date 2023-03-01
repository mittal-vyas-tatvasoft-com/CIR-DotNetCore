using CIR.Application.Services.GlobalConfiguration;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationWeekendController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationWeekendService iGlobalConfigurationWeekendService;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationWeekendController(IGlobalConfigurationWeekendService globalConfigurationWeekendService)
        {
            iGlobalConfigurationWeekendService = globalConfigurationWeekendService;
        }
        #endregion

        #region METHODS

        /// <summary>
        /// This method filters the weekends based on the given conditions.
        /// </summary>
        /// <param name="displayLength"></param>
        /// <param name="displayStart"></param>
        /// <param name="sortCol"></param>
        /// <param name="search"></param>
        /// <param name="countryCodeId"></param>
        /// <param name="dayOfWeekId"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWeekends(int displayLength, int displayStart, string? sortCol, string? search, int? countryCodeId, int? dayOfWeekId, bool? sortAscending = true)
        {
            try
            {
                return await iGlobalConfigurationWeekendService.GetGlobalConfigurationWeekends(displayLength, displayStart, sortCol, countryCodeId, dayOfWeekId, search, sortAscending);
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes weeknd details as parameters and creates Weekends and returns that weekend
        /// </summary>
        /// <param name="weekend"> this object contains different parameters as details of a weekends </param>
        /// <returns > created Weekends </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] GlobalConfigurationWeekend weekend)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var weekendExists = await iGlobalConfigurationWeekendService.CountryWiseWeekendsExists(weekend.CountryId, weekend.DayOfWeekId);
                    if (weekendExists)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = string.Format(SystemMessages.msgDataExists, "Weekend") });
                    }
                    else
                    {
                        return await iGlobalConfigurationWeekendService.CreateGlobalConfigurationWeekends(weekend);
                    }
                }
                catch (Exception ex)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method disables Weekend 
        /// </summary>
        /// <param name="id"> Weekend will be disabled according to this id </param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await iGlobalConfigurationWeekendService.DeleteGlobalConfigurationWeekend(id);
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }
        #endregion
    }
}
