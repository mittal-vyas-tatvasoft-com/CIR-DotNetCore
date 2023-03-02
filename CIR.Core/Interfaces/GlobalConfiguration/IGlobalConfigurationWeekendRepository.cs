using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
    public interface IGlobalConfigurationWeekendRepository
    {
        Task<IActionResult> GetGlobalConfigurationWeekends(int displayLength, int displayStart, string? sortCol, int? countryNameId, int? countryCodeId, string? search,int? dayOfWeekId, bool? sortAscending);
        Task<Boolean> CountryWiseWeekendsExists(long countryId, long dayOfWeekId);
        Task<IActionResult> CreateGlobalConfigurationWeekends(GlobalConfigurationWeekend globalConfigurationWeekend);
        Task<IActionResult> DeleteGlobalConfigurationWeekend(int id);
    }
}
