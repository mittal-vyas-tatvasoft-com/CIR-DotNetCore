using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Application.Services.GlobalConfiguration
{
	public class GlobalConfigurationFieldsService : IGlobalConfigurationFieldsService
	{
		private readonly IGlobalConfigurationFieldsRepository globalConfigurationFieldsRepository;

		public GlobalConfigurationFieldsService(IGlobalConfigurationFieldsRepository iGlobalConfigurationFieldsRepository)
		{
			globalConfigurationFieldsRepository = iGlobalConfigurationFieldsRepository;
		}
		public async Task<IActionResult> GetAllGlobalConfigurationFields()
		{
			return await globalConfigurationFieldsRepository.GetAllGlobalConfigurationFields();
		}

		public async Task<IActionResult> CreateOrUpdateGlobalConfigurationFields(List<GlobalConfigurationField> globalConfigurationFields)
		{
			return await globalConfigurationFieldsRepository.CreateOrUpdateGlobalConfigurationFields(globalConfigurationFields);
		}
	}
}
