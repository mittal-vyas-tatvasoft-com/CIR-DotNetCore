using CIR.Core.Interfaces.GlobalConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Application.Services.GlobalConfiguration
{
    public class GlobalConfigurationMessagesService:IGlobalConfigurationMessagesService
    {
        private readonly IGlobalConfigurationMessagesRepository globalConfigurationMessagesRepository;

        public GlobalConfigurationMessagesService(IGlobalConfigurationMessagesRepository iGlobalConfigurationMessagesRepository)
        {
            globalConfigurationMessagesRepository = iGlobalConfigurationMessagesRepository;
        }
    }
}
