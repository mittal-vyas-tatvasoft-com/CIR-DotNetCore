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
    public class GlobalConfigurationWeekendRepository : IGlobalConfigurationWeekendRepository
    {
        #region PROPERTIES
        private readonly CIRDbContext cIRDbContext;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationWeekendRepository(CIRDbContext context)
        {
            cIRDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region METHODS

        /// <summary>
        /// This method retuns filtered Weekends list
        /// </summary>
        /// <param name="displayLength"> how many row/data we want to fetch(for pagination) </param>
        /// <param name="displayStart"> from which row we want to fetch(for pagination) </param>
        /// <param name="sortCol"> name of column which we want to sort</param>
        /// <param name="search"> word that we want to search in Weekends table </param>
        /// <param name="sortAscending"> 'asc' or 'desc' direction for sort </param>
        /// <param name="countryCodeId">used to filter weekends list based on country code Id</param>
        /// <param name="countryNameId">used to filter weekends list based on country name Id</param>
        /// <returns> filtered list of Weekends </returns>
        public async Task<IActionResult> GetGlobalConfigurationWeekends(int displayLength, int displayStart, string? sortCol, int? countryNameId, int? countryCodeId, string? search, bool? sortAscending)
        {
            try
            {
                GlobalConfigurationWeekendModel filteredWeekendsList = new GlobalConfigurationWeekendModel();
                if (string.IsNullOrEmpty(sortCol))
                {
                    sortCol = "Id";
                }
                List<WeekendViewModel> weekend = new List<WeekendViewModel>();
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@DisplayLength", displayLength);
                        parameters.Add("@DisplayStart", displayStart);
                        parameters.Add("@SortCol", sortCol);
                        parameters.Add("@Search", search);
                        parameters.Add("@SortDir", sortAscending);
                        parameters.Add("@CountryNameId", countryNameId);
                        parameters.Add("@CountryCodeId", countryCodeId);
                        weekend = await Task.FromResult(connection.Query<WeekendViewModel>("spGetFilteredWeekends", parameters, commandType: CommandType.StoredProcedure).ToList());
                    }
                }
                filteredWeekendsList.WeekendList = weekend;
                filteredWeekendsList.Count = weekend.Count;

                if (filteredWeekendsList.WeekendList.Count > 0)
                {
                    return new JsonResult(new CustomResponse<GlobalConfigurationWeekendModel>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = filteredWeekendsList });
                }
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Weekends") });
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
		/// This method is used by check countrywise weekend already exist or not
		/// </summary>
		/// <param name="countryId"></param>
		/// <param name="dayOfWeekId"></param>
		/// <returns></returns>
        public async Task<bool> CountryWiseWeekendsExists(long countryId, long dayOfWeekId)
        {
            var result = false;
            using (DbConnection dbConnection = new DbConnection())
            {
                using (var connection = dbConnection.Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CountryId", countryId);
                    parameters.Add("@DayOfWeekId", dayOfWeekId);
                    result = await Task.FromResult(Convert.ToBoolean(connection.ExecuteScalar("spCountryWiseWeekendExists", parameters, commandType: CommandType.StoredProcedure)));
                }
            }
            return result;
        }

        /// <summary>
		/// This method is used by create method of globalconfiguration weekend
		/// </summary>
		/// <param name="globalConfigurationWeekends"> new weekends data for weekend </param>
		/// <returns> Success status if its valid else failure </returns>
        public async Task<IActionResult> CreateGlobalConfigurationWeekends(GlobalConfigurationWeekend globalConfigurationWeekend)
        {
            try
            {
                var result = 0;
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@CountryId", globalConfigurationWeekend.CountryId);
                        parameters.Add("@DayOfWeekId", globalConfigurationWeekend.DayOfWeekId);
                        result = await Task.FromResult(connection.Execute("spCreateGlobalConfigurationWeekend", parameters, commandType: CommandType.StoredProcedure));
                    }
                }
                if (result == 1)
                {
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Weekend") });
                }
                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = string.Format(SystemMessages.msgSavingDataError, "Weekend") });
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
		/// This method used by delete globalconfiguration weekend
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task<IActionResult> DeleteGlobalConfigurationWeekend(int id)
        {

            try
            {
                if (cIRDbContext.GlobalConfigurationWeekends.Any(x => x.Id == id))
                {
                    var result = 0;
                    using (DbConnection dbConnection = new DbConnection())
                    {
                        using (var connection = dbConnection.Connection)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@WeekendId", id);
                            result = await Task.FromResult(connection.Execute("spDeleteWeekend", parameters, commandType: CommandType.StoredProcedure));
                        }
                    }
                    if (result != 0)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = string.Format(SystemMessages.msgDataDeletedSuccessfully, "Weekend") });
                    }
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity.GetDescriptionAttribute() });
                }
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Weekend") });
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        #endregion
    }
}
