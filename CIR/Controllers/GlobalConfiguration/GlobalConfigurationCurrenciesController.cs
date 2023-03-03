using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class GlobalConfigurationCurrenciesController : ControllerBase
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
        /// This method takes country Id as input and gives currencies available in that country
        /// </summary>
        /// <param name="countryId">Id of a available country</param>
        /// <returns>list of currencies country Id wise</returns>
        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetGlobalConfigurationCurrenciesCountryWise(int countryId)
        {
            try
            {
                return await globalConfigurationCurrenciesService.GetGlobalConfigurationCurrenciesCountryWise(countryId);
            }
            catch 
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method updates global currency
        /// </summary>
        /// <param name="globalConfigurationCurrencyModels">this object contains different parameters as details of a list of globalcurrencies</param>
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
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method adds new currency 
        /// </summary>
        /// <param name="currency">this object containes currency code name and symbol as parameter</param>
        /// <returns>Success status if input is valid else failure status </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddNewCurrency(Currency currency)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    currency.CodeName = currency.CodeName.ToUpper();

                    if (MyRegex().Match(currency.CodeName).Success)
                    {
                        var isExist = await globalConfigurationCurrenciesService.CurrencyExists(currency.CodeName);
                        if (isExist)
                        {
                            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = string.Format(SystemMessages.msgDataExists, "Currency") });
                        }
                        else
                        {
                            return await globalConfigurationCurrenciesService.AddNewCurrency(currency);
                        }
                    }
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.invalidCurrencyCodeName });
                }
                catch
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        [GeneratedRegex("^[A-Z]{3}$")]
        private static partial Regex MyRegex();

        #endregion

    }
}
