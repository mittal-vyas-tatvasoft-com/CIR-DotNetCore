using CIR.Core.Interfaces.GlobalConfiguration;

namespace CIR.Application.Services.GlobalConfiguration
{
    public class GlobalConfigurationFieldsService : IGlobalConfigurationFieldsService
    {
        private readonly IGlobalConfigurationFieldsRepository globalConfigurationFieldsRepository;

        public GlobalConfigurationFieldsService(IGlobalConfigurationFieldsRepository iGlobalConfigurationFieldsRepository)
        {
            globalConfigurationFieldsRepository = iGlobalConfigurationFieldsRepository;
        }

    }
}
