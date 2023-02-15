using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationCutOffTimesController : ControllerBase
    {
        #region PROPERTIES

        private readonly IGlobalConfigurationCutOffTimesService _globalConfigurationCutOffTimesService;

        #endregion

        #region CONSTRUCTOR

        public GlobalConfigurationCutOffTimesController(IGlobalConfigurationCutOffTimesService globalConfigurationCutOffTimesService)
        {
            _globalConfigurationCutOffTimesService= globalConfigurationCutOffTimesService;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// This method takes a get globalconfiguration cuttoftime countrywise
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns> cutofftime </returns> 
        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetGlobalConfigurationCutOffTimeByCountryWise(int countryId)
        {
            try
            {
                return await _globalConfigurationCutOffTimesService.GetGlobalConfigurationCutOffTimeByCountryWise(countryId);
            }
            catch (Exception ex)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
            }
        }

        /// <summary>
        /// This method takes CutOffTime as request and save data.
        /// </summary>
        /// <param name="globalConfigurationCutOffTimeModel">This object contains different parameters as details of a CutOffTime</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] GlobalConfigurationCutOffTimeModel globalConfigurationCutOffTimeModel) 
        {
            if(ModelState.IsValid)
            {
                try
                {
                    return await _globalConfigurationCutOffTimesService.CreateOrUpdateGlobalConfigurationCutOffTime(globalConfigurationCutOffTimeModel);
                }
                catch (Exception ex)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.BadRequest.GetDescriptionAttribute(), Data = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method takes CutOffTime as request and update data.
        /// </summary>
        /// <param name="globalConfigurationCutOffTimeModel">This object contains different parameters as details of a CutOffTime</param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] GlobalConfigurationCutOffTimeModel globalConfigurationCutOffTimeModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _globalConfigurationCutOffTimesService.CreateOrUpdateGlobalConfigurationCutOffTime(globalConfigurationCutOffTimeModel);
                }
                catch (Exception ex)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.BadRequest.GetDescriptionAttribute(), Data = SystemMessages.msgBadRequest });
        }
        #endregion
    }
}
