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
        private readonly IGlobalConfigurationCutOffTimesRepository iGlobalConfigurationCutOffTimesRepository;

        public GlobalConfigurationCutOffTimesService(IGlobalConfigurationCutOffTimesRepository globalConfigurationCutOffTimesRepository)
        {
            iGlobalConfigurationCutOffTimesRepository = globalConfigurationCutOffTimesRepository;
        }

        public async Task<IActionResult> GetGlobalConfigurationCutOffTimeByCountryWise(int countryId)
        {
            return await iGlobalConfigurationCutOffTimesRepository.GetGlobalConfigurationCutOffTimeByCountryWise(countryId);
        }

        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationCutOffTime(GlobalConfigurationCutOffTimeModel globalConfigurationCutOffTimeModel)
        {
            return await iGlobalConfigurationCutOffTimesRepository.CreateOrUpdateGlobalConfigurationCutOffTime(globalConfigurationCutOffTimeModel);
        }
    }
}
