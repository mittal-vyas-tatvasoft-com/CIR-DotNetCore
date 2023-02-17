using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CIR.Data.Data.GlobalConfiguration
{
    public class GlobalConfigurationCurrenciesRepository : IGlobalConfigurationCurrenciesRepository
    {
        #region PROPERTIES

        private readonly CIRDbContext cIRDBContext;

        #endregion

        #region CONSTRUCTORS
        public GlobalConfigurationCurrenciesRepository(CIRDbContext context)
        {
            cIRDBContext = context ??
                throw new ArgumentNullException(nameof(context));
        }

        #endregion


        #region METHODS

        /// <summary>
        /// This method is used by get currency List by countryId method in contoller
        /// </summary>
        /// <param name="countryId">Id of a available country</param>
        /// <returns>list of currencies country Id wise</returns>
        public async Task<IActionResult> GetGlobalConfigurationCurrenciesCountryWise(int countryId)
        {
            try
            {
                List<GlobalConfigurationCurrencyModel> globalConfigurationCurrenciesList;

                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@countryId", countryId);
                        globalConfigurationCurrenciesList = await Task.FromResult(connection.Query<GlobalConfigurationCurrencyModel>("spGetGlobalConfigurationCurrenciesByCountryId", parameters, commandType: CommandType.StoredProcedure).ToList());
                    }
                }

                if (globalConfigurationCurrenciesList.Count == 0)
                {
                    return new JsonResult(new CustomResponse<List<GlobalConfigurationCurrencyModel>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Country") });
                }
                return new JsonResult(new CustomResponse<List<GlobalConfigurationCurrencyModel>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = globalConfigurationCurrenciesList });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method is used by create method and update method of globalcurrency controller
        /// </summary>
        /// <param name="globalConfigurationCurrencyModels">this object contains different parameters as details of a list of globalcurrencies</param>
        /// <returns>Success status if input is valid else failure status</returns>

        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationCurrencies(List<GlobalConfigurationCurrencyModel> globalConfigurationCurrencyModels)
        {
            try
            {
                if (globalConfigurationCurrencyModels.Any(x => x.CountryId == 0 || x.CurrencyId == 0))
                {
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
                }
                if (globalConfigurationCurrencyModels != null)
                {
                    var result = 0;
                    foreach (var item in globalConfigurationCurrencyModels)
                    {
                        using (DbConnection dbConnection = new DbConnection())
                        {
                            using (var connection = dbConnection.Connection)
                            {
                                DynamicParameters parameters = new DynamicParameters();
                                parameters.Add("@CountryId", item.CountryId);
                                parameters.Add("@CurrencyId", item.CurrencyId);
                                parameters.Add("@Enabled", item.Enabled);

                                result = await Task.FromResult(connection.Execute("spCreateOrUpdateGlobalConfigurationCurrencies", parameters, commandType: CommandType.StoredProcedure));
                            }
                        }
                    }
                    if (result == 1)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Global Configuration Currencies") });
                    }
                }
                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method is used in add new currency method of controller to check if input currency already exists or not
        /// </summary>
        /// <param name="codeName">name of a currency</param>
        /// <returns>returns true if currency exists else returns false</returns>
        public async Task<Boolean> CurrencyExists(string codeName)
        {
            var result = false;
            using (DbConnection dbConnection = new DbConnection())
            {
                using (var connection = dbConnection.Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CodeName", codeName);
                    result = await Task.FromResult(Convert.ToBoolean(connection.ExecuteScalar("spCurrencyExists", parameters, commandType: CommandType.StoredProcedure)));
                }
                return result;
            }
        }

        /// <summary>
        /// This method is used by add new currency method of controller to add new currency if not available
        /// </summary>
        /// <param name="currency">this object containes currency code name and symbol as parameter</param>
        /// <returns>>Success status if input is valid else failure status</returns>
        public async Task<IActionResult> AddNewCurrency(Currency currency)
        {
            try
            {
                if (!String.IsNullOrEmpty(currency.CodeName) && !String.IsNullOrEmpty(currency.Symbol))
                {
                    var result = 0;
                    using (DbConnection dbConnection = new DbConnection())
                    {
                        using (var connection = dbConnection.Connection)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@CodeName", currency.CodeName);
                            parameters.Add("@Symbol", currency.Symbol);

                            result = await Task.FromResult(connection.Execute("spAddNewCurrency", parameters, commandType: CommandType.StoredProcedure));
                        }
                    }
                    if (result == 1)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Currency") });
                    }
                }
                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        #endregion



    }
}
