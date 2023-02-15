using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationEmailsController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationEmailsService iGlobalConfigurationEmailsService;
        #endregion

        #region CONSTRUCTORS
        public GlobalConfigurationEmailsController(IGlobalConfigurationEmailsService globalConfigurationEmailsService)
        {
            iGlobalConfigurationEmailsService = globalConfigurationEmailsService;
        }
        #endregion

        #region METHODS

        /// <summary>
        /// This method takes a get globalconfiguration email list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{cultureId}")]
        public async Task<IActionResult> Get(int cultureId)
        {
            try
            {
                return await iGlobalConfigurationEmailsService.GetGlobalConfigurationEmailsDataList(cultureId);
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes a add globalconfiguration Emails
        /// </summary>
        /// <param name="globalConfigurationEmails"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(List<GlobalConfigurationEmails> globalConfigurationEmails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await iGlobalConfigurationEmailsService.CreateOrUpdateGlobalConfigurationEmails(globalConfigurationEmails);
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });

        }

        /// <summary>
        /// This method takes a update globalconfiguration Emails
        /// </summary>
        /// <param name="globalConfigurationEmails"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(List<GlobalConfigurationEmails> globalConfigurationEmails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await iGlobalConfigurationEmailsService.CreateOrUpdateGlobalConfigurationEmails(globalConfigurationEmails);
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
