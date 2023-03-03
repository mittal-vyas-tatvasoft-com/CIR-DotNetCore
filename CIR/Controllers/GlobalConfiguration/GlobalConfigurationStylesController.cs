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
    public class GlobalConfigurationStylesController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationStylesService globalConfigurationStylesService;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationStylesController(IGlobalConfigurationStylesService _globalConfigurationStylesService)
        {
            globalConfigurationStylesService = _globalConfigurationStylesService;
        }
        #endregion

        #region METHODS

        /// <summary>
        /// This method takes a get globalconfiguration styles list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGlobalConfigurationStylesList()
        {
            try
            {
                return await globalConfigurationStylesService.GetGlobalConfigurationStyles();

            }
            catch 
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes a Update globalconfiguration styles
        /// </summary>
        /// <param name="globalConfigurationStyles"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateGlobalConfigurationStylesList([FromBody] List<GlobalConfigurationStyle> globalConfigurationStyles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationStylesService.UpdateGlobalConfigurationStyles(globalConfigurationStyles);
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
