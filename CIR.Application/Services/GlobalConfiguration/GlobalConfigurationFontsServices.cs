using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;

namespace CIR.Application.Services.GlobalConfiguration
{
    public class GlobalConfigurationFontsServices: IGlobalConfigurationFontsServices
    {
        private readonly IGlobalConfigurationFontsRepository _globalConfigurationFontsRepository;
        public GlobalConfigurationFontsServices(IGlobalConfigurationFontsRepository globalConfigurationFontsRepository)
        {
            _globalConfigurationFontsRepository = globalConfigurationFontsRepository;
        }
        public async Task<IActionResult> GetGlobalConfigurationFonts()
        {
            return await _globalConfigurationFontsRepository.GetGlobalConfigurationFonts();
        }
        public async Task<IActionResult> UpdateGlobalConfigurationFonts(List<GlobalConfigurationFonts> globalConfigurationFonts)
        {
            return await _globalConfigurationFontsRepository.UpdateGlobalConfigurationFonts(globalConfigurationFonts);
        }

        public async Task<IActionResult> CreateGlobalConfigurationFonts(GlobalConfigurationFonts globalConfigurationFonts)
        {
            return await _globalConfigurationFontsRepository.CreateGlobalConfigurationFonts(globalConfigurationFonts);
        }
    }
}
