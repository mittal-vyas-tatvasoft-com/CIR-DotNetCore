using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        #region PROPERTIES

        private readonly ICommonService commonService;

        #endregion

        #region CONSTRUCTORS

        public CommonController(ICommonService commonService)
        {
            this.commonService = commonService;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// This method is used to get list of all available currencies
        /// </summary>
        /// <returns>returns list of all available currencies</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCurrencies()
        {
            try
            {
                return await commonService.GetCurrencies();
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method returns the list of countries
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                return await commonService.GetCountries();
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method get a culture list
        /// </summary>
        /// <returns>CultureList</returns>
        [HttpGet("GetCultures")]
        public async Task<IActionResult> GetCultures()
        {
            try
            {
                return await commonService.GetCultures();
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        #endregion

    }
}
