using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CIR.Data.Data.GlobalConfiguration
{
	public class GlobalConfigurationFieldsRepository : IGlobalConfigurationFieldsRepository
	{
		#region PROPERTIES
		private readonly CIRDbContext cIRDbContext;
		#endregion

		#region CONSTRUCTOR
		public GlobalConfigurationFieldsRepository(CIRDbContext context)
		{
			cIRDbContext = context ??
			   throw new ArgumentNullException(nameof(context));
		}

		#endregion

		#region METHODS

		/// <summary>
		/// This method used by GetAllGlobalConfigurationFields
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetAllGlobalConfigurationFields()
		{
			try
			{
				List<GlobalConfigurationField> globalConfigurationField = new();

				using (DbConnection dbConnection = new DbConnection())
				{
					using (var connection = dbConnection.Connection)
					{
						globalConfigurationField = connection.Query<GlobalConfigurationField>("spGetGlobalConfigurationFields", null, commandType: CommandType.StoredProcedure).ToList();
					}
				}

				List<GlobalConfigurationFieldsModel> globalConfigurationFieldsList = new List<GlobalConfigurationFieldsModel>();
				if (globalConfigurationField.Count == 0)
				{
					return new JsonResult(new CustomResponse<List<GlobalConfigurationFieldsModel>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Global Configuration Fields") });
				}

				foreach (var item in globalConfigurationField)
				{
					GlobalConfigurationFieldsModel fields = new GlobalConfigurationFieldsModel();
					fields.FieldName = Enum.GetName(typeof(GlobalConfigurationEnums.Fields), item.FieldTypeId);
					fields.Id = item.Id;
					fields.FieldTypeId = item.FieldTypeId;
					fields.Enabled = item.Enabled;
					fields.Required = item.Required;
					globalConfigurationFieldsList.Add(fields);
				}
				return new JsonResult(new CustomResponse<List<GlobalConfigurationFieldsModel>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = globalConfigurationFieldsList });
			}
			catch
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
			}
		}



		/// <summary>
		/// This method takes a create or update globalconfiguration Fields
		/// </summary>
		/// <param name="globalConfigurationFields"></param>
		/// <returns></returns>

		public async Task<IActionResult> CreateOrUpdateGlobalConfigurationFields(List<GlobalConfigurationField> globalConfigurationFields)
		{
			try
			{
				if (globalConfigurationFields != null)
				{
					var result = 0;
					foreach (var item in globalConfigurationFields)
					{
						using (DbConnection dbConnection = new DbConnection())
						{
							using (var connection = dbConnection.Connection)
							{
								DynamicParameters parameters = new DynamicParameters();
								parameters.Add("@Id", item.Id);
								parameters.Add("@FieldTypeId", item.FieldTypeId);
								parameters.Add("@Enabled", item.Enabled);
								parameters.Add("@Required", item.Required);
								result = connection.Execute("spCreateOrUpdateGlobalConfigurationFields", parameters, commandType: CommandType.StoredProcedure);
							}
						}
					}
					if (result == 1)
					{
						return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Global Configuration Fields") });
					}
				}
				return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
			}
			catch
			{
				return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
			}
		}
		#endregion
	}
}
