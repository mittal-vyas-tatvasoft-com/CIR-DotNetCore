using CIR.Common.Data;
using CIR.Core.ViewModel.Users;
using System.Data;

namespace CIR.Common.Helper
{
    public class RolesTreeViewList
    {
        /// <summary>
        /// This method takes a get role list data 
        /// </summary>
        /// <param name="roleListDatatable"></param>
        /// <returns></returns>
        public static List<SubRolesModel> GetRolesListData(DataTable roleListDatatable)
        {
            List<SubRolesModel> subRoleList = new List<SubRolesModel>();

            var listMain = SQLHelper.ConvertToGenericModelList<SubModel>(roleListDatatable);
            var groupData = listMain.GroupBy(x => x.GroupId);
            foreach (var item in groupData)
            {
                var siteGroup = listMain.Where(x => x.GroupId == item.Key).GroupBy(x => x.SiteId);
                SubRolesModel subRole = new SubRolesModel
                {
                    site = new List<RoleGrouping2SubSiteModel>()
                };
                foreach (var itemSite in siteGroup)
                {
                    RoleGrouping2SubSiteModel roleGrouping2SubSiteModel = new RoleGrouping2SubSiteModel
                    {
                        SiteId = itemSite.Key,
                        Languages = new List<RoleGrouping2CultureModel>()
                    };

                    var cultureGroup = listMain.Where(x => x.GroupId == item.Key && x.SiteId == itemSite.Key).GroupBy(x => x.CultureId);
                    foreach (var itemCulture in cultureGroup)
                    {
                        RoleGrouping2CultureModel roleGrouping2CultureModel = new RoleGrouping2CultureModel
                        {
                            CultureId = itemCulture.Key,
                            Privileges = new List<RoleGrouping2PermissionModel>()
                        };

                        var privilegesGroup = listMain.Where(x => x.GroupId == item.Key && x.SiteId == itemSite.Key && x.CultureId == itemCulture.Key).GroupBy(x => x.PrivilegesId);
                        foreach (var itemprivileges in privilegesGroup)
                        {
                            RoleGrouping2PermissionModel roleGrouping2PermissionModel = new RoleGrouping2PermissionModel
                            {
                                PrivilegesId = itemprivileges.Key
                            };
                            roleGrouping2CultureModel.Privileges.Add(roleGrouping2PermissionModel);
                        }
                        roleGrouping2SubSiteModel.Languages.Add(roleGrouping2CultureModel);
                    }
                    subRole.site.Add(roleGrouping2SubSiteModel);
                }
                subRole.groupId = listMain.Where(x => x.GroupId == item.Key).FirstOrDefault().GroupId;
                subRoleList.Add(subRole);
            }
            return subRoleList;
        }
    }
}
