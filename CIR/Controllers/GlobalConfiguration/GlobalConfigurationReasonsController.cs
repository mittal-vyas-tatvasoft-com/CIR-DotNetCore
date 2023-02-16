using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;


namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GlobalConfigurationReasonsController : Controller
    {
        #region PROPERTIES

        private readonly IGlobalConfigurationReasonsService globalConfigurationReasonsService;

        #endregion

        #region CONSTRUCTORS

        public GlobalConfigurationReasonsController(IGlobalConfigurationReasonsService iglobalConfigurationReasonsService)
        {
            globalConfigurationReasonsService = iglobalConfigurationReasonsService;
        }

        #endregion


        #region METHODS

        /// <summary>
        /// This method takes a get globalconfiguration reasons list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGlobalConfigurationReasons()
        {
            try
            {
                return await globalConfigurationReasonsService.GetGlobalConfigurationReasons();
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes a create globalconfiguration reason
        /// </summary>
        /// <param name="globalConfigurationReasons"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(List<GlobalConfigurationReason> globalConfigurationReasons)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationReasonsService.CreateOrUpdateGlobalConfigurationReasons(globalConfigurationReasons);
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }


        /// <summary>
        /// This method takes a update globalconfiguration reason
        /// </summary>
        /// <param name="globalConfigurationReasons"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(List<GlobalConfigurationReason> globalConfigurationReasons)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationReasonsService.CreateOrUpdateGlobalConfigurationReasons(globalConfigurationReasons);
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        #endregion

    }
}
