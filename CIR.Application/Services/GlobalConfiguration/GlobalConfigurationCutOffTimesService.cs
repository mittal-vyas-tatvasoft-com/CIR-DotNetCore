using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Application.Services.GlobalConfiguration
{
    public class GlobalConfigurationCutOffTimesService : IGlobalConfigurationCutOffTimesService
    {
        private readonly IGlobalConfigurationCutOffTimesRepository _globalConfigurationCutOffTimesRepository;

        public GlobalConfigurationCutOffTimesService(IGlobalConfigurationCutOffTimesRepository globalConfigurationCutOffTimesRepository)
        {
            _globalConfigurationCutOffTimesRepository = globalConfigurationCutOffTimesRepository;
        }

        public async Task<IActionResult> GetGlobalConfigurationCutOffTimeByCountryWise(int countryId)
        {
            return await _globalConfigurationCutOffTimesRepository.GetGlobalConfigurationCutOffTimeByCountryWise(countryId);
        }

        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationCutOffTime(GlobalConfigurationCutOffTimeModel globalConfigurationCutOffTimeModel)
        {
            return await _globalConfigurationCutOffTimesRepository.CreateOrUpdateGlobalConfigurationCutOffTime(globalConfigurationCutOffTimeModel);
        }
    }
}
