﻿using CIR.Application.Services.GlobalConfiguration;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationCurrenciesController : ControllerBase
    {
        #region PROPERTIES

        private readonly IGlobalConfigurationCurrenciesService globalConfigurationCurrenciesService;

        #endregion

        #region CONSTRUCTORS

        public GlobalConfigurationCurrenciesController(IGlobalConfigurationCurrenciesService _globalConfigurationCurrenciesService)
        {
            globalConfigurationCurrenciesService = _globalConfigurationCurrenciesService;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// This method takes get global currency country wise
        /// </summary>
        /// <param name="countryId">this object contains countryId</param>
        /// <returns>list of currencies country Id wise</returns>
        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetGlobalConfigurationCurrenciesCountryWise(int countryId)
        {
            try
            {
                return await globalConfigurationCurrenciesService.GetGlobalConfigurationCurrenciesCountryWise(countryId);
            }
            catch (Exception ex)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
            }
        }

        /// <summary>
        /// This method takes add global currency
        /// </summary>
        /// <param name="globalConfigurationCurrencyModels">this object contains different parameters as details of a globalcurrency</param>
        /// <returns>Success status if input is valid else failure status</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(List<GlobalConfigurationCurrencyModel> globalConfigurationCurrencyModels)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationCurrenciesService.CreateOrUpdateGlobalConfigurationCurrencies(globalConfigurationCurrencyModels);
                }
                catch (Exception ex)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.BadRequest.GetDescriptionAttribute(), Data = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method takes update global currency
        /// </summary>
        /// <param name="globalConfigurationCurrencyModels">this object contains different parameters as details of a globalcurrency</param>
        /// <returns>Success status if input is valid else failure status</returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(List<GlobalConfigurationCurrencyModel> globalConfigurationCurrencyModels)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationCurrenciesService.CreateOrUpdateGlobalConfigurationCurrencies(globalConfigurationCurrencyModels);
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