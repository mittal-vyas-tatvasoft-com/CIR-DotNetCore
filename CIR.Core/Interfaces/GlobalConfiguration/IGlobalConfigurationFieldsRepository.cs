using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
	public interface IGlobalConfigurationFieldsRepository
	{
		Task<IActionResult> GetAllGlobalConfigurationFields();
		Task<IActionResult> CreateOrUpdateGlobalConfigurationFields(List<GlobalConfigurationField> globalConfigurationFields);
	}
}
