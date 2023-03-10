using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Interfaces.Utilities;
using CIR.Core.ViewModel.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.Utilities
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingsLookupsController : ControllerBase
    {
        #region PROPERTIES
        private readonly ISystemSettingsLookupsService lookupService;
        #endregion

        #region CONSTRUCTOR
        public SystemSettingsLookupsController(ISystemSettingsLookupsService lookupService)
        {
            lookupService = lookupService;
        }
        #endregion

        #region METHODS
        [HttpGet("[action]")]
        /// <summary>
        /// This method takes Lookups details and updates the LookupItem
        /// </summary>
        /// <param name="cultureId"></param>
        /// <param name="code"></param>
        /// <param name="searchLookupItems"></param>
        /// <param name="sortAscending"></param>
        /// <returns> get LookupItemList </returns>
        public async Task<IActionResult> GetLookupItemList(long cultureId, string code, string? searchLookupItems, bool sortAscending = true)
        {
            try
            {
                searchLookupItems ??= string.Empty;
                return await lookupService.GetAllLookupsItems(cultureId, code, searchLookupItems, sortAscending);
            }
            catch {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes LookupItemsText details and add LookupItemsText
        /// </summary>
        /// <param name="lookupItemsTextmodel"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(LookupItemsTextModel lookupItemsTextmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var isExist = await lookupService.LookupItemExists(lookupItemsTextmodel.CultureId, lookupItemsTextmodel.LookupItemId);
                    if (isExist)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message =  string.Format(SystemMessages.msgDataExists, "Lookup Item") });
                    }
                    else
                    {
                        return await lookupService.CreateOrUpdateLookupItem(lookupItemsTextmodel);
                    }
                }
                catch {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method takes Lookups details and updates the LookupItem
        /// </summary>
        /// <param name="lookupItemsTextmodel"> this object contains different parameters as details of a lookupItem </param>
        /// <returns> updated LookupItems </returns>

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(LookupItemsTextModel lookupItemsTextmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await lookupService.CreateOrUpdateLookupItem(lookupItemsTextmodel);
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return await lookupService.GetLookupById(id);
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        #endregion
    }
}
