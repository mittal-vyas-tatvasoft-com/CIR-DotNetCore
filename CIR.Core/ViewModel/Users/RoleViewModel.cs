namespace CIR.Core.ViewModel.Users
{
    public class RoleViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long WrongLoginAttempts { get; set; }
        public bool AllPermissions { get; set; }
        public int TotalCount { get; set; }
    }
    public class RoleModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long WrongLoginAttempts { get; set; }
        public bool AllPermissions { get; set; }

    }
    public class RolePrivilegesMModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public long Value { get; set; }
    }
}
