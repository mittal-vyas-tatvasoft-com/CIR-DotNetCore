using CIR.Core.Interfaces.Utilities;
using CIR.Core.ViewModel.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Application.Services.Utilities
{
	public class SystemSettingsLanguagesService : ISystemSettingsLanguagesService
	{
		#region PROPERTIES
		private readonly ISystemSettingsLanguagesRepository isystemSettingsLanguagesRepository;
		#endregion

		#region CONSTRUCTOR
		public SystemSettingsLanguagesService(ISystemSettingsLanguagesRepository repository)
		{
			isystemSettingsLanguagesRepository = repository;
		}
		#endregion

		#region METHODS
		public Task<IActionResult> UpdateSystemSettingsLanguage(List<CulturesModel> culture)
		{
			return isystemSettingsLanguagesRepository.UpdateSystemSettingsLanguage(culture);
		}
		#endregion
	}
}
