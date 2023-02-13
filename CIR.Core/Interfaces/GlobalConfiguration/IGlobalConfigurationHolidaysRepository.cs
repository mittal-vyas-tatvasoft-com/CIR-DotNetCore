using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
    public interface IGlobalConfigurationHolidaysRepository
    {
        Task<IActionResult> GetGlobalConfigurationHolidayById(long id);
        Task<IActionResult> GetAllGlobalConfigurationHolidays(int displayLength, int displayStart, string? sortCol, string? search, int? countryCodeId, int? countryNameId, bool? sortAscending = true);
        Task<IActionResult> CreateOrUpdateGlobalConfigurationHoliday(Holidays holiday);
        Task<IActionResult> DeleteGlobalConfigurationHoliday(long id);
    }
}
