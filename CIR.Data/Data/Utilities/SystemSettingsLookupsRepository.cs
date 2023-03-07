using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.Utilities;
using CIR.Core.Interfaces.Utilities;
using CIR.Core.ViewModel.Utilities;
using CIR.Data.Data.Common;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace CIR.Data.Data.Utilities
{
    public class SystemSettingsLookupsRepository : ISystemSettingsLookupsRepository
    {
      
        #region METHODS

        /// <summary>
        /// This method is used by create and update methods of Lookups
        /// </summary>
        /// <param name="lookupItemsTextModel></param>
        /// <returns>Success status if its valid else failure</returns>
        public async Task<IActionResult> CreateOrUpdateLookupItem(LookupItemsTextModel lookupItemsTextModel)
        {
            try
            {
                if (lookupItemsTextModel.Code == null || lookupItemsTextModel.CultureId == 0)
                {
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.BadRequest, Result = false, Message =  SystemMessages.msgBadRequest });
                }
                var result = 0;
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@Id", lookupItemsTextModel.Id);
                        parameters.Add("@SystemCodeId", lookupItemsTextModel.SystemCodeId);
                        parameters.Add("@LookupItemId", lookupItemsTextModel.LookupItemId);
                        parameters.Add("@CultureId", lookupItemsTextModel.CultureId);
                        parameters.Add("@Text", lookupItemsTextModel.Text);
                        parameters.Add("@Active", lookupItemsTextModel.Active);
                        result = connection.Execute("spCreateOrUpdateLookupItem", parameters, commandType: CommandType.StoredProcedure);

                    }
                }
                if (result != 0)
                {
                    return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Saved, Result = true, Message = string.Format(SystemMessages.msgDataSavedSuccessfully, "Lookup Item") });
                }
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message =SystemMessages.msgBadRequest });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.UnprocessableEntity, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method retuns filtered LookupItems list
        /// </summary>
        /// <param name="code">Defalut Loaded Code=Salutation-type, It will change base on dropdown selection change</param>
        /// <param name="cultureId"> Default CultureId = 1 , It will change base on Culture dropdown change</param>
        /// <param name="searchLookupItems"> filter LookupItemList</param>
        /// <param name="sortAscending"> filter LookupItemList</param>
        /// <returns> filtered list of LookupItemsList </returns>
        public async Task<IActionResult> GetAllLookupsItems(long cultureId, string code, string? searchLookupItems, bool sortAscending = true)
        {
            try
            {
                List<LookupItemsText> lookupsItemList = new();
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@CultureId", cultureId);
                        parameters.Add("@Code", code);
                        parameters.Add("@SearchLookupItems", searchLookupItems);
                        lookupsItemList = connection.Query<LookupItemsText>("spGetLookupItemList", parameters, commandType: CommandType.StoredProcedure).ToList();
                    }
                }

                if (lookupsItemList.Count == 0)
                {
                    return new JsonResult(new CustomResponse<LookupItemsText>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = String.Format(SystemMessages.msgDataNotExists, "LookUp Items") });
                }

                return new JsonResult(new CustomResponse<List<LookupItemsText>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = lookupsItemList });
            }
            catch {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        /// <summary>
        /// This method retuns filtered LookupItems list
        /// </summary>
        /// <param name="code">Defalut Loaded Code=null, It will change base on dropdown selection change</param>
        /// <param name="cultureId"> Default CultureId = 0 , It will change base on Culture dropdown change</param>
        /// <param name="searchCultureCode"> filter CultureCodeList</param>
        /// <returns> filtered list of LookupItemsList </returns>
        private List<CulturalCodesModel> GetCulturalCodesList(long? cultureId, string code, string searchCultureCode = null)
        {
            List<CulturalCodesModel> codeList = new();

            using (DbConnection dbConnection = new DbConnection())
            {
                using (var connection = dbConnection.Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CultureId", cultureId);
                    parameters.Add("@Code", code);
                    parameters.Add("@SearchCultureCode", searchCultureCode);
                    codeList = connection.Query<CulturalCodesModel>("spGetCultureCodeList", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            return codeList;
        }

        /// <summary>
        /// this meethod checks if LookupItem exists or not based on input cultureId,LookupItemId
        /// </summary>
        /// <param name="cultureId"></param>
        /// <param name="lookupItemId"></param>
        /// <returns> if LookupItem exists true else false </returns>
        public async Task<Boolean> LookupItemExists(long cultureId, long lookupItemId)
        {
            using (DbConnection dbConnection = new DbConnection())
            {
                using (var connection = dbConnection.Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CultureId", cultureId);
                    parameters.Add("@LookupItemId", lookupItemId);
                    var checkItemExist = connection.Query("spLookupItemExists", parameters, commandType: CommandType.StoredProcedure);
                    if (checkItemExist.FirstOrDefault() != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// This method will return look up items of given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetLookupById(int id)
        {
            try
            {
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@id", id);
                        List<LookupItemsTextModel> lookupItem = connection.Query<LookupItemsTextModel>("spGetLookupById", parameters, commandType: CommandType.StoredProcedure).ToList();
                        if (lookupItem.Count == 0)
                        {
                            return new JsonResult(new CustomResponse<string>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message =  string.Format(SystemMessages.msgNotFound, "Lookup Item") });
                        }
                        return new JsonResult(new CustomResponse<List<LookupItemsTextModel>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = lookupItem });
                    }
                }
            }
            catch {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }
        #endregion
    }
}
