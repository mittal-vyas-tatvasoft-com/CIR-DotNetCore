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
    public class GlobalConfigurationMessagesController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationMessagesService globalConfigurationMessagesService;

        #endregion
        #region CONSTRUCTOR
        public GlobalConfigurationMessagesController(IGlobalConfigurationMessagesService _globalConfigurationMessagesServices)
        {
            globalConfigurationMessagesService = _globalConfigurationMessagesServices;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// This method takes a get globalconfiguration messages list
        /// </summary>
        /// <param name="cultureId"></param>
        /// <returns></returns>
        [HttpGet("{cultureId}")]
        public async Task<IActionResult> GetGlobalConfigurationMessagesList(int cultureId)
        {
            try
            {
                return await globalConfigurationMessagesService.GetGlobalConfigurationMessagesList(cultureId);
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes a create globalconfiguration messages
        /// </summary>
        /// <param name="globalConfigurationMessages"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(List<GlobalConfigurationMessage> globalConfigurationMessages)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationMessagesService.CreateOrUpdateGlobalConfigurationMessages(globalConfigurationMessages);
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method takes a update globalconfiguration messages
        /// </summary>
        /// <param name="globalConfigurationMessages"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(List<GlobalConfigurationMessage> globalConfigurationMessages)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationMessagesService.CreateOrUpdateGlobalConfigurationMessages(globalConfigurationMessages);
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
