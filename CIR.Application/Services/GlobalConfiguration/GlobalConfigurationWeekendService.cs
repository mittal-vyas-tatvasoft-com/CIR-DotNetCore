using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Application.Services.GlobalConfiguration
{
    public class GlobalConfigurationWeekendService : IGlobalConfigurationWeekendService
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationWeekendRepository iGlobalConfigurationWeekendRepository;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationWeekendService(IGlobalConfigurationWeekendRepository globalConfigurationWeekendRepository)
        {
            iGlobalConfigurationWeekendRepository = globalConfigurationWeekendRepository;
        }
        #endregion

        #region METHODS
        public async Task<bool> CountryWiseWeekendsExists(long countryId, long dayOfWeekId)
        {
            return await iGlobalConfigurationWeekendRepository.CountryWiseWeekendsExists(countryId, dayOfWeekId);
        }

        public async Task<IActionResult> CreateGlobalConfigurationWeekends(GlobalConfigurationWeekend globalConfigurationWeekend)
        {
            return await iGlobalConfigurationWeekendRepository.CreateGlobalConfigurationWeekends(globalConfigurationWeekend);
        }

        public async Task<IActionResult> DeleteGlobalConfigurationWeekend(int id)
        {
            return await iGlobalConfigurationWeekendRepository.DeleteGlobalConfigurationWeekend(id);
        }

/*        public async Task<IActionResult> GetGlobalConfigurationWeekends(int displayLength, int displayStart, string? sortCol, int? countryNameId, int? countryCodeId, int? dayOfWeekId, string? search, bool? sortAscending)
        {
            return await iGlobalConfigurationWeekendRepository.GetGlobalConfigurationWeekends(displayLength, displayStart, sortCol, countryNameId, countryCodeId, dayOfWeekId, search, sortAscending);
        }*/

        public async Task<IActionResult> GetGlobalConfigurationWeekends(int displayLength, int displayStart, string? sortCol, int? countryNameId, int? countryCodeId, string? search, int? dayOfWeekId, bool? sortAscending)
        {
            return await iGlobalConfigurationWeekendRepository.GetGlobalConfigurationWeekends(displayLength, displayStart, sortCol, countryNameId, countryCodeId, search, dayOfWeekId, sortAscending);
        }
        #endregion
    }
}
