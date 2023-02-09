
using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.EntityFrameworkCore;


namespace CIR.Common.Data
{
    public class CIRDbContext : DbContext
    {
        public CIRDbContext(DbContextOptions<CIRDbContext> options) : base(options) { }
        public DbSet<Holidays> Holidays { get; set; }


    }
}


