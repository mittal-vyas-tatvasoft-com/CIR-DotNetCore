using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationHolidaysController : ControllerBase
    {
        #region PROPERTIES

        private readonly IGlobalConfigurationHolidaysService globalConfigurationHolidaysService;

        #endregion

        #region CONSTRUCTORS

        public GlobalConfigurationHolidaysController(IGlobalConfigurationHolidaysService globalConfigurationHolidaysService)
        {
            this.globalConfigurationHolidaysService = globalConfigurationHolidaysService;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// This method generates the model from uploaded file and adds the records in holidays
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        [HttpPost("AddCsv")]
        public async Task<IActionResult> AddCsv(IFormFile uploadedFile)
        {
            try
            {
                var filExtension = Path.GetExtension(uploadedFile.FileName);
                var fileName = Guid.NewGuid().ToString() + filExtension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    uploadedFile.CopyTo(fs);
                }
                if (filExtension == ".csv" || filExtension == ".xlsx")
                {
                    using (var reader = new StreamReader(filePath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var holidayRecords = csv.GetRecords<Holidays>();
                        foreach (var holidayRecord in holidayRecords)
                        {
                            if (holidayRecord == null)
                            {
                                break;
                            }
                            await globalConfigurationHolidaysService.CreateOrUpdateGlobalConfigurationHoliday(holidayRecord);
                        }
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Holiday") });
                    }
                }
                else
                {
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgSelectXlsxOrCSVFile });
                }
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method filters the holidays based on the given conditions.
        /// </summary>
        /// <param name="displayLength"></param>
        /// <param name="displayStart"></param>
        /// <param name="sortCol"></param>
        /// <param name="search"></param>
        /// <param name="countryCodeId"></param>
        /// <param name="countryNameId"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(int displayLength, int displayStart, string? sortCol, string? search, int? countryCodeId, int? countryNameId, bool? sortAscending = true)
        {
            try
            {
                return await globalConfigurationHolidaysService.GetAllGlobalConfigurationHolidays(displayLength, displayStart, sortCol, search, countryCodeId, countryNameId, sortAscending);
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method fetches the holiday with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                return await globalConfigurationHolidaysService.GetGlobalConfigurationHolidayById(id);
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method is used to create a new holiday
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(Holidays holiday)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationHolidaysService.CreateOrUpdateGlobalConfigurationHoliday(holiday);
                }
                catch (Exception)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method is used to update the holiday
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(Holidays holiday)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await globalConfigurationHolidaysService.CreateOrUpdateGlobalConfigurationHoliday(holiday);
                }
                catch (Exception)
                {
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
                }
            }
            return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
        }

        /// <summary>
        /// This method is used to delete the holiday
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                return await globalConfigurationHolidaysService.DeleteGlobalConfigurationHoliday(id);
            }
            catch (Exception)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        #endregion
    }
}
