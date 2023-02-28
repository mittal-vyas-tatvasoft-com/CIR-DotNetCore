using CIR.Core.ViewModel.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.Utilities
{
	public interface ISystemSettingsLanguagesRepository
	{
		Task<IActionResult> UpdateSystemSettingsLanguage(List<CulturesModel> cultureList);

	}
}
