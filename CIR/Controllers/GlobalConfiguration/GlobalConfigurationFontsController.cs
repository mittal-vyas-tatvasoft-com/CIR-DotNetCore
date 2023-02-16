using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationFontsController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationFontsServices globalConfigurationFontsServices;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationFontsController(IGlobalConfigurationFontsServices globalConfigurationFontsServices)
        {
            globalConfigurationFontsServices = globalConfigurationFontsServices;
        }
        #endregion

        #region METHODS

        /// <summary>
        /// This method takes a get globalconfiguration fonts list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return await globalConfigurationFontsServices.GetGlobalConfigurationFonts();
            }
            catch (Exception ex)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method takes a create globalconfiguration fonts
        /// </summary>
        /// <param name="globalConfigurationFonts"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(GlobalConfigurationFonts globalConfigurationFonts)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationFontsServices.CreateGlobalConfigurationFonts(globalConfigurationFonts);
                }
                catch (Exception ex)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method takes a update globalconfiguration fonts
        /// </summary>
        /// <param name="globalConfigurationFonts"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(List<GlobalConfigurationFonts> globalConfigurationFonts)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationFontsServices.UpdateGlobalConfigurationFonts(globalConfigurationFonts);
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
