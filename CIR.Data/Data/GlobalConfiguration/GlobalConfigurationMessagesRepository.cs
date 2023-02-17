using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Data.Data.GlobalConfiguration
{
    public class GlobalConfigurationMessagesRepository : IGlobalConfigurationMessagesRepository
    {
        #region PROPERTIES
        private readonly CIRDbContext cIRDbContext;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationMessagesRepository(CIRDbContext context)
        {
            cIRDbContext = context ??
               throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region METHODS
        /// <summary>
        /// This method used by get globalconfiguration messages list
        /// </summary>
        /// <param name="cultureId"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetGlobalConfigurationMessagesList(int cultureId)
        {
            try
            {

                List<GlobalConfigurationMessageModel> globalConfigurationMessagesList;
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@CultureId", cultureId);
                        globalConfigurationMessagesList = await Task.FromResult(connection.Query<GlobalConfigurationMessageModel>("spGetGlobalConfigurationMessagesListByCultureId", parameters, commandType: CommandType.StoredProcedure).ToList());
                    }
                }

                if (globalConfigurationMessagesList.Count == 0)
                {
                    return new JsonResult(new CustomResponse<List<GlobalConfigurationMessageModel>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "Culture") });
                }
                return new JsonResult(new CustomResponse<List<GlobalConfigurationMessageModel>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = globalConfigurationMessagesList });

            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method is used by create or update globalConfiguration Messages
        /// </summary>
        /// <param name="globalConfigurationMessages"></param>
        /// <returns>Success status if its valid else failure</returns>
        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationMessages(List<GlobalConfigurationMessage> globalConfigurationMessages)
        {
            try
            {
                if (globalConfigurationMessages != null)
                {
                    var result = 0;
                    using (DbConnection dbConnection = new DbConnection())
                    {
                        using (var connection = dbConnection.Connection)
                        {
                            foreach (var item in globalConfigurationMessages)
                            {
                                DynamicParameters parameters = new DynamicParameters();
                                parameters.Add("@Id", item.Id);
                                parameters.Add("@Type", item.Type);
                                parameters.Add("@Content", item.Content);
                                parameters.Add("@CultureId", item.CultureId);
                                result = await Task.FromResult(connection.Execute("spCreateOrUpdateGlobalConfigurationMessages", parameters, commandType: CommandType.StoredProcedure));
                            }
                        }
                        if (result == 1)
                        {
                            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Global Configuration Messages") });
                        }
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
