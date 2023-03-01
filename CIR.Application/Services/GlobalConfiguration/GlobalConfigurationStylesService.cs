using CIR.Core.Interfaces.GlobalConfiguration;
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
    }
}
