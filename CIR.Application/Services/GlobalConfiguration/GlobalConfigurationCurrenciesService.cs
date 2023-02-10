﻿using CIR.Core.Entities.GlobalConfiguration;
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
    public class GlobalConfigurationCurrenciesService : IGlobalConfigurationCurrenciesService
    {
        private readonly IGlobalConfigurationCurrenciesRepository globalConfigurationCurrenciesRepository;
        public GlobalConfigurationCurrenciesService(IGlobalConfigurationCurrenciesRepository _globalConfigurationCurrenciesRepository)
        {
            globalConfigurationCurrenciesRepository = _globalConfigurationCurrenciesRepository;
        }

        public async Task<IActionResult> GetGlobalConfigurationCurrenciesCountryWise(int countryId)
        {
            return await globalConfigurationCurrenciesRepository.GetGlobalConfigurationCurrenciesCountryWise(countryId);
        }

        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationCurrencies(List<GlobalConfigurationCurrencyModel> globalConfigurationCurrencyModels)
        {
            return await globalConfigurationCurrenciesRepository.CreateOrUpdateGlobalConfigurationCurrencies(globalConfigurationCurrencyModels);
        }
    }
}
