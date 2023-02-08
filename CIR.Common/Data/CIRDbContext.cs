
using CIR.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;


namespace CIR.Common.Data
{
    public class CIRDbContext : DbContext
    {
        public CIRDbContext(DbContextOptions<CIRDbContext> options) : base(options) { }
        public DbSet<Roles> Roles
        {
            get;
            set;
        }
        public DbSet<RoleGrouping> RolesGroupings
        {
            get;
            set;
        }
        public DbSet<RoleGrouping2Culture> RoleGrouping2Cultures
        {
            get;
            set;
        }

        public DbSet<RoleGrouping2Permission> RoleGrouping2Permissions
        {
            get;
            set;
        }
        public DbSet<RoleGrouping2SubSite> RoleGrouping2SubSites
        {
            get;
            set;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleGrouping2SubSite>().HasKey(x => new { x.RoleGroupingId, x.SubSiteId });
            modelBuilder.Entity<RoleGrouping2Permission>().HasKey(x => new { x.RoleGroupingId, x.PermissionEnumId });
            modelBuilder.Entity<RoleGrouping2Culture>().HasKey(x => new { x.RoleGroupingId, x.CultureLcid });
        }
    }
}


