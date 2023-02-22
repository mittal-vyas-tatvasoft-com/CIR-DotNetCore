using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CIR.Data.Data.GlobalConfiguration
{
    public class GlobalConfigurationReasonsRepository : IGlobalConfigurationReasonsRepository
    {
        #region PROPERTIES

        private readonly CIRDbContext cIRDbContext;

        #endregion

        #region CONSTRUCTORS

        public GlobalConfigurationReasonsRepository(CIRDbContext context)
        {
            cIRDbContext = context ??
                throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region METHODS

        /// <summary>
        /// This method takes a get globalconfiguration reasons list
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetGlobalConfigurationReasons()
        {
            try
            {
                List<GlobalConfigurationReason> globalConfigurationReasonsList;
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        globalConfigurationReasonsList = connection.Query<GlobalConfigurationReason>("spGetGlobalConfigurationReasons", null, commandType: CommandType.StoredProcedure).ToList();
                    }
                }

                if (globalConfigurationReasonsList.Count == 0)
                {
                    return new JsonResult(new CustomResponse<List<GlobalConfigurationReason>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgNotFound, "Reasons") });
                }

                return new JsonResult(new CustomResponse<List<GlobalConfigurationReason>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = globalConfigurationReasonsList });

            }
            catch (Exception ex)
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }


        /// <summary>
        /// This method takes a create or update globalconfiguration reasons
        /// </summary>
        /// <param name="globalConfigurationReasons"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationReasons(List<GlobalConfigurationReason> globalConfigurationReasons)
        {
            try
            {
                if (globalConfigurationReasons != null)
                {
                    var result = 0;
                    foreach (var option in globalConfigurationReasons)
                    {
                        using (DbConnection dbConnection = new DbConnection())
                        {
                            using (var connection = dbConnection.Connection)
                            {
                                DynamicParameters parameters = new DynamicParameters();
                                parameters.Add("@Id", option.Id);
                                parameters.Add("@Type", option.Type);
                                parameters.Add("@Enabled", option.Enabled);
                                parameters.Add("@Content", option.Content);

                                result = connection.Execute("spCreateOrUpdateGlobalConfigurationReasons", parameters, commandType: CommandType.StoredProcedure);
                            }
                        }
                    }
                    if (result != 0)
                    {
                        return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Reasons") });
                    }
                    return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = string.Format(SystemMessages.msgSavingDataError, "Reasons") });
                }
                return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message = SystemMessages.msgEnterValidData });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        #endregion
    }
}

