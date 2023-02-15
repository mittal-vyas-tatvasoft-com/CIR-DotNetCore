using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// This method used by getcurrency List countryid wise
        /// </summary>
        /// <param name="countryId"></param>
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
        /// <param name="globalConfigurationCurrencyModels"></param>
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
        #endregion



    }
}
