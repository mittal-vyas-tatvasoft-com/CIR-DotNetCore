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
    public class GlobalConfigurationHolidaysRepository : IGlobalConfigurationHolidaysRepository
    {
        #region PROPERTIES

        private readonly CIRDbContext cirDbContext;

        #endregion

        #region CONSTRUCTORS

        public GlobalConfigurationHolidaysRepository(CIRDbContext cirDbContext)
        {
            this.cirDbContext = cirDbContext ??
                throw new ArgumentNullException(nameof(cirDbContext));
        }

        #endregion

        #region METHODS

        /// <summary>
        /// This method creates or updates the holiday
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationHoliday(Holidays holiday)
        {
            try
            {
                var result = 0;
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@Id", holiday.Id);
                        parameters.Add("@CountryId", holiday.CountryId);
                        parameters.Add("@Date", holiday.Date);
                        parameters.Add("@Description", holiday.Description);
                        result = await Task.FromResult(connection.Execute("spCreateOrUpdateGlobalConfigurationHolidays", parameters, commandType: CommandType.StoredProcedure));
                    }
                }
                if (result != 0)
                {
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Holiday") });
                }
                return new JsonResult(new CustomResponse<Holidays>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity.GetDescriptionAttribute() });
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method deletes the holiday with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteGlobalConfigurationHoliday(long id)
        {
            try
            {
                if (cirDbContext.Holidays.Any(x => x.Id == id))
                {
                    var result = 0;
                    using (DbConnection dbConnection = new DbConnection())
                    {
                        using (var connection = dbConnection.Connection)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@Id", id);
                            result = await Task.FromResult(connection.Execute("spDeleteHolidays", parameters, commandType: CommandType.StoredProcedure));
                        }
                    }
                    if (result != 0)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = string.Format(SystemMessages.msgDataDeletedSuccessfully, "Holiday") });
                    }
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity.GetDescriptionAttribute() });
                }
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Holiday") });
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method fetches the filtered list of the holidays based on given conditions.
        /// </summary>
        /// <param name="displayLength"></param>
        /// <param name="displayStart"></param>
        /// <param name="sortCol"></param>
        /// <param name="search"></param>
        /// <param name="countryCodeId"></param>
        /// <param name="countryNameId"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetAllGlobalConfigurationHolidays(int displayLength, int displayStart, string? sortCol, string? search, int? countryCodeId, int? countryNameId, bool? sortAscending = true)
        {
            try
            {
                HolidayModel filteredHolidays = new HolidayModel();
                if (string.IsNullOrEmpty(sortCol))
                {
                    sortCol = "Id";
                }
                List<HolidayViewModel> holidays = new List<HolidayViewModel>();
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
                        parameters.Add("@CountryCodeId", countryCodeId);
                        parameters.Add("@CountryNameId", countryNameId);
                        holidays = await Task.FromResult(connection.Query<HolidayViewModel>("spGetFilteredHolidays", parameters, commandType: CommandType.StoredProcedure).ToList());
                    }
                }
                filteredHolidays.Holidays = holidays;
                filteredHolidays.TotalCount = holidays.Count;

                if (filteredHolidays.Holidays.Count == 0)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Holiday") });
                }
                return new JsonResult(new CustomResponse<HolidayModel>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = filteredHolidays });
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method fetches the holiday with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetGlobalConfigurationHolidayById(long id)
        {
            try
            {
                Holidays holiday;
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@Id", id);
                        holiday = await Task.FromResult(connection.Query<Holidays>("spGetHolidayById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault());
                    }
                }
                if (holiday == null)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Holiday") });
                }
                return new JsonResult(new CustomResponse<Holidays>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = holiday });

            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        #endregion
    }
}
