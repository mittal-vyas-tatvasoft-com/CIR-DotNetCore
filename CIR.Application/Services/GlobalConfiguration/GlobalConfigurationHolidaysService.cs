using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Application.Services.GlobalConfiguration
{
    public class GlobalConfigurationHolidaysService : IGlobalConfigurationHolidaysService
    {
        private readonly IGlobalConfigurationHolidaysRepository globalConfigurationHolidaysRepository;

        public GlobalConfigurationHolidaysService(IGlobalConfigurationHolidaysRepository globalConfigurationHolidaysRepository)
        {
            this.globalConfigurationHolidaysRepository = globalConfigurationHolidaysRepository;
        }

        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationHoliday(Holidays holiday)
        {
            return await globalConfigurationHolidaysRepository.CreateOrUpdateGlobalConfigurationHoliday(holiday);
        }

        public async Task<IActionResult> DeleteGlobalConfigurationHoliday(long id)
        {
            return await globalConfigurationHolidaysRepository.DeleteGlobalConfigurationHoliday(id);
        }

        public async Task<IActionResult> GetAllGlobalConfigurationHolidays(int displayLength, int displayStart, string? sortCol, string? search, int? countryCodeId, int? countryNameId, bool? sortAscending = true)
        {
            return await globalConfigurationHolidaysRepository.GetAllGlobalConfigurationHolidays(displayLength, displayStart, sortCol, search, countryCodeId, countryNameId, sortAscending);
        }

        public async Task<IActionResult> GetGlobalConfigurationHolidayById(long id)
        {
            return await globalConfigurationHolidaysRepository.GetGlobalConfigurationHolidayById(id);
        }
    }
}
