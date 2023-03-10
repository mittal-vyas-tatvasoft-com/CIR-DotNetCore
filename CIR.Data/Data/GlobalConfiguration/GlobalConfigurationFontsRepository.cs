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
	public class GlobalConfigurationFontsRepository : ControllerBase, IGlobalConfigurationFontsRepository
	{
		#region PROPERTIES
		private readonly CIRDbContext cIRDBContext;
		#endregion

		#region CONSTRUCTOR
		public GlobalConfigurationFontsRepository(CIRDbContext context)
		{
            cIRDBContext = context ??
				throw new ArgumentNullException(nameof(context));
		}
		#endregion

		#region METHODS

		/// <summary>
		/// This method is used by get globalconfiguration fonts list
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetGlobalConfigurationFonts()
		{
			try
			{
				List<GlobalConfigurationFonts> globalConfigurationFontsList;
				using (DbConnection dbConnection = new DbConnection())
				{
					using (var connection = dbConnection.Connection)
					{
						globalConfigurationFontsList = await Task.FromResult(connection.Query<GlobalConfigurationFonts>("spGetGlobalConfigurationFonts", null, commandType: CommandType.StoredProcedure).ToList());
                        
					}
				}

				return new JsonResult(new CustomResponse<List<GlobalConfigurationFonts>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = globalConfigurationFontsList });
			}
			catch
			{
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
		}


        /// <summary>
        /// This method used by create globalconfiguration fonts
        /// </summary>
        /// <param name="globalConfigurationFonts"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateGlobalConfigurationFonts(GlobalConfigurationFonts globalConfigurationFont)
        {
            try
            {
                if (globalConfigurationFont.Name == string.Empty)
                {
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgEnterValidData });
                }
                if (globalConfigurationFont != null)
                {
                    var result = 0;
                    using (DbConnection dbConnection = new DbConnection())
                    {
                        using (var connection = dbConnection.Connection)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@Id", 0);
                            parameters.Add("@Name", globalConfigurationFont.Name);
                            parameters.Add("@FontFamily", globalConfigurationFont.FontFamily);
                            parameters.Add("@FontFileName", globalConfigurationFont.FontFileName);
                            parameters.Add("@IsDefault", globalConfigurationFont.IsDefault);
                            parameters.Add("@Enabled", globalConfigurationFont.Enabled);

                            result = await Task.FromResult(connection.Execute("spCreateOrUpdateGlobalConfigurationFonts", parameters, commandType: CommandType.StoredProcedure));
                        }
                    }
                    if (result != 0)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Global Configuration Fonts") });
                    }
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity.GetDescriptionAttribute() });
                }
                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgBadRequest });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method used by update globalconfiguration fonts
        /// </summary>
        /// <param name="globalConfigurationFonts"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateGlobalConfigurationFonts(List<GlobalConfigurationFonts> globalConfigurationFonts)
		{
			try
			{
				if (globalConfigurationFonts.Any(x => x.Name == string.Empty))
				{
					return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgEnterValidData });
				}
				if (globalConfigurationFonts != null)
				{
					var result = 0;
					foreach (var item in globalConfigurationFonts)
					{
						using (DbConnection dbConnection = new DbConnection())
						{
							using (var connection = dbConnection.Connection)
							{
								DynamicParameters parameters = new DynamicParameters();
								parameters.Add("@Id", item.Id);
								parameters.Add("@Name", item.Name);
								parameters.Add("@FontFamily", item.FontFamily);
								parameters.Add("@FontFileName", item.FontFileName);
                                parameters.Add("@IsDefault", item.IsDefault);
								parameters.Add("@Enabled", item.Enabled);

                                result = await Task.FromResult(connection.Execute("spCreateOrUpdateGlobalConfigurationFonts", parameters, commandType: CommandType.StoredProcedure));
							}
						}
					}
					if (result != 0)
					{
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Global Configuration Fonts") });
					}
					return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity.GetDescriptionAttribute() });
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
