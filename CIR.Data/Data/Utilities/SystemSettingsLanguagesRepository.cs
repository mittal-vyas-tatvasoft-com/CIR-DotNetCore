﻿using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.Utilities;
using CIR.Core.Interfaces.Utilities;
using CIR.Core.ViewModel.Utilities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CIR.Data.Data.Utilities
{
	public class SystemSettingsLanguagesRepository : ISystemSettingsLanguagesRepository
	{
		#region PROPERTIES
		private readonly CIRDbContext cIRDbContext;
		#endregion

		#region CONSTRUCTOR
		public SystemSettingsLanguagesRepository(CIRDbContext context)
		{
			cIRDbContext = context ??
			throw new ArgumentNullException(nameof(context));
		}
		#endregion

		#region METHODS

		/// <summary>
		/// This method is used by Update method of SystemSettings Languages
		/// </summary>
		/// <param name="cultureList"> List of Culture</param>
		/// <returns></returns>
		public async Task<IActionResult> UpdateSystemSettingsLanguage(List<CulturesModel> cultureList)
		{
			try
			{
				var cultureData = GetListForUpdatedLanguages(cultureList);
				if (cultureData != null)
				{
					var result = 0;
					foreach (Culture item in cultureData)
					{
						using (DbConnection dbConnection = new DbConnection())
						{
							using (var connection = dbConnection.Connection)
							{
								DynamicParameters parameters = new DynamicParameters();
								parameters.Add("@Id", item.Id);
								parameters.Add("@ParentId", item.ParentId);
								parameters.Add("@Name", item.Name);
								parameters.Add("@DisplayName", item.DisplayName);
								parameters.Add("@Enabled", item.Enabled);
								parameters.Add("@NativeName", item.NativeName);
								result = connection.Execute("spUpdateSystemSettingsLanguage", parameters, commandType: CommandType.StoredProcedure);
							}
						}
					}
					if (result != 0)
					{
						return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Cultures") });
					}
					return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = string.Format(SystemMessages.msgSavingDataError) });
				}
				else
				{
					return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = true, Message = string.Format(SystemMessages.msgNotFound) });
				}
			}
			catch
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
			}
		}

		/// <summary>
		/// This method is used for Getting Updated List for Languages
		/// </summary>
		/// <param name="culturesModels" ></param>
		/// <returns></returns>
		public List<Culture> GetListForUpdatedLanguages(List<CulturesModel> culturesModels)
		{
			List<Culture> list = new List<Culture>();

			foreach (CulturesModel item in culturesModels)
			{
				using (DbConnection dbConnection = new DbConnection())
				{
					using (var connection = dbConnection.Connection)
					{
						DynamicParameters parameters = new DynamicParameters();
						parameters.Add("@Id", item.Id);
						List<Culture> culture = connection.Query<Culture>("spGetListForUpdatedLanguages", parameters, commandType: CommandType.StoredProcedure).ToList();

						if (culture != null)
						{
							culture.FirstOrDefault().Enabled = item.Enabled;
							list.Add(culture.FirstOrDefault());
						}
					}
				}
			}
			return list;
		}
		#endregion
	}
}