using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Application.Services.GlobalConfiguration
{
    public class GlobalConfigurationReasonsService : IGlobalConfigurationReasonsService
    {
        private readonly IGlobalConfigurationReasonsRepository globalConfigurationReasonsRepository;

        public GlobalConfigurationReasonsService(IGlobalConfigurationReasonsRepository iglobalConfigurationReasonsRepository)
        {
            globalConfigurationReasonsRepository = iglobalConfigurationReasonsRepository;
        }

        public async Task<IActionResult> GetGlobalConfigurationReasons()
        {
            return await globalConfigurationReasonsRepository.GetGlobalConfigurationReasons();
        }

        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationReasons(List<GlobalConfigurationReason> globalConfigurationReasons)
        {
            return await globalConfigurationReasonsRepository.CreateOrUpdateGlobalConfigurationReasons(globalConfigurationReasons);
        }
    }
}

