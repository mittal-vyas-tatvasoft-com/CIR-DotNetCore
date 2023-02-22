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
    public class GlobalConfigurationMessagesService : IGlobalConfigurationMessagesService
    {
        private readonly IGlobalConfigurationMessagesRepository globalConfigurationMessagesRepository;

        public GlobalConfigurationMessagesService(IGlobalConfigurationMessagesRepository _globalConfigurationMessagesRepository)
        {
            globalConfigurationMessagesRepository = _globalConfigurationMessagesRepository;
        }
        public async Task<IActionResult> GetGlobalConfigurationMessagesList(int cultureId)
        {
            return await globalConfigurationMessagesRepository.GetGlobalConfigurationMessagesList(cultureId);
        }
        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationMessages(List<GlobalConfigurationMessage> globalConfigurationMessages)
        {
            return await globalConfigurationMessagesRepository.CreateOrUpdateGlobalConfigurationMessages(globalConfigurationMessages);
        }
    }
}
