using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
	public interface IGlobalConfigurationFieldsService
	{
		Task<IActionResult> GetAllGlobalConfigurationFields();
		Task<IActionResult> CreateOrUpdateGlobalConfigurationFields(List<GlobalConfigurationField> globalConfigurationFields);
	}
}
