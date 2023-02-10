namespace CIR.Core.ViewModel.Users
{
    public class RolePermissionModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long WrongLoginAttempts { get; set; }
        public Boolean AllPermissions { get; set; } = false;
        public List<SubRolesModel> Roles { get; set; }
    }
}
