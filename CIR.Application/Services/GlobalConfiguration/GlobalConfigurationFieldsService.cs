using CIR.Core.Interfaces.GlobalConfiguration;

namespace CIR.Application.Services.GlobalConfiguration
{
	public class GlobalConfigurationFieldsService : IGlobalConfigurationFieldsService
	{
		private readonly IGlobalConfigurationFieldsRepository _globalConfigurationFieldsRepository;

		public GlobalConfigurationFieldsService(IGlobalConfigurationFieldsRepository globalConfigurationFieldsRepository)
		{
			_globalConfigurationFieldsRepository = globalConfigurationFieldsRepository;
		}

	}
}
