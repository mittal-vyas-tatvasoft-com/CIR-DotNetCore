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
    public class GlobalConfigurationStylesService : IGlobalConfigurationStylesService
    {
        private readonly IGlobalConfigurationStylesRepository globalConfigurationStylesService;

        public GlobalConfigurationStylesService(IGlobalConfigurationStylesRepository _globalConfigurationStylesService)
        {
            globalConfigurationStylesService = _globalConfigurationStylesService;
        }
        public async Task<IActionResult> GetGlobalConfigurationStyles()
        {
            return await globalConfigurationStylesService.GetGlobalConfigurationStyles();
        }
        public async Task<IActionResult> UpdateGlobalConfigurationStyles(List<GlobalConfigurationStyle> globalConfigurationStyles)
        {
            return await globalConfigurationStylesService.UpdateGlobalConfigurationStyles(globalConfigurationStyles);
        }
    }
}
