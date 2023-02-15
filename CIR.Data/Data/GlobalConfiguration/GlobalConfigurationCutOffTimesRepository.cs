using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Data.Data.GlobalConfiguration
{
    public class GlobalConfigurationCutOffTimesRepository : IGlobalConfigurationCutOffTimesRepository
    {
        #region PROPERTIES
        private readonly CIRDbContext _cIRDbContext;
        private readonly IConfiguration _configuration;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationCutOffTimesRepository(CIRDbContext context, IConfiguration configuration)
        {
            _cIRDbContext= context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
        }
        #endregion

        #region METHODS

        /// <summary>
        /// This method takes a get globalconfiguration cuttoftime countrywise
        /// </summary>
        /// <param name="countryId"></params>
        /// <returns></returns>
        public async Task<IActionResult> GetGlobalConfigurationCutOffTimeByCountryWise(int countryId)
        {
            try
            {
                GlobalConfigurationCutOffTime? cutOffTime = new();
                using(DbConnection dbConnection = new DbConnection())
                {
                    using(var connection = dbConnection.Connection) 
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@CountryId", countryId);
                        cutOffTime = await Task.FromResult(connection.Query<GlobalConfigurationCutOffTime>("spGetGlobalConfigurationCutOffTimesByCountryId", parameters, commandType: CommandType.StoredProcedure).ToList().FirstOrDefault());
                    }
                }
                if (cutOffTime != null)
                {
                    return new JsonResult(new CustomResponse<GlobalConfigurationCutOffTime>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = cutOffTime });
                }
                else
                {
                    GlobalConfigurationCutOffTimeModel cutOffTimeModel = new()
                    {
                        CountryId = countryId,
                        CutOffTime = _configuration.GetSection("StaticCutOffTime").GetSection("CutOffTime").Value,
                        CutOffDay = (int)GlobalConfigurationEnums.CutOffDays.SameDay
                    };
                    return new JsonResult(new CustomResponse<GlobalConfigurationCutOffTimeModel>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = cutOffTimeModel });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
            }
        }

        ///<summary>
        ///This method takes create or update global configuration cutoff time
        ///</summary>
        ///<param name="globalConfigurationCutOffTimeModel"></param>
		/// <returns>Success status if its valid else failure</returns>
        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationCutOffTime(GlobalConfigurationCutOffTimeModel globalConfigurationCutOffTimeModel)
        {
            try
            {
                if (IsStringNullorEmpty(globalConfigurationCutOffTimeModel.CutOffTime) || globalConfigurationCutOffTimeModel.CountryId == 0)
                {
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.BadRequest.GetDescriptionAttribute(), Data = SystemMessages.msgEnterValidData });
                }
                string result = "";
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@Id", globalConfigurationCutOffTimeModel.Id);
                        parameters.Add("@CountryId", globalConfigurationCutOffTimeModel.CountryId);
                        parameters.Add("@CutOffTime", globalConfigurationCutOffTimeModel.CutOffTime);
                        parameters.Add("@CutOffDay", globalConfigurationCutOffTimeModel.CutOffDay);

                        result = Convert.ToString(connection.ExecuteScalar("spCreateOrUpdateGlobalConfigurationCutOffTimes", parameters, commandType: CommandType.StoredProcedure));
                    }
                }
                if (result != "False")
                {
                    if (globalConfigurationCutOffTimeModel.Id > 0)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = string.Format(SystemMessages.msgDataUpdatedSuccessfully, "GlobalConfiguration CutOffTimes") });
                    }
                    else
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = string.Format(SystemMessages.msgDataSavedSuccessfully, "GlobalConfiguration CutOffTimes") });
                    }
                }
                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity.GetDescriptionAttribute(), Data = SystemMessages.msgBadRequest });
            }
            catch (Exception ex)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.InternalServerError.GetDescriptionAttribute(), Data = ex });
            }
        }


        /// <summary>
        /// This method used by check Is StringNullorEmpty
        /// </summary>
        /// <returns></returns>
        public Boolean IsStringNullorEmpty(string value)
        {
            if (value == null || value == string.Empty)
                return true;
            else
                return false;
        }
        #endregion
    }
}
