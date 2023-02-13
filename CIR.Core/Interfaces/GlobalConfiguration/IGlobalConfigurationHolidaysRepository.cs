using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
    public interface IGlobalConfigurationHolidaysRepository
    {
        public Task<IActionResult> GetGlobalConfigurationHolidayById(long id);
        public Task<IActionResult> GetAllGlobalConfigurationHolidays(int displayLength, int displayStart, string? sortCol, string? search, int? countryCodeId, int? countryNameId, bool? sortAscending = true);
        public Task<IActionResult> CreateOrUpdateGlobalConfigurationHoliday(Holidays holiday);
        public Task<IActionResult> DeleteGlobalConfigurationHoliday(long id);
    }
}
