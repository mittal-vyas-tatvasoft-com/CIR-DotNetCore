﻿
using CIR.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;


namespace CIR.Common.Data
{
	public class CIRDbContext : DbContext
	{
		public CIRDbContext(DbContextOptions<CIRDbContext> options) : base(options) { }
		public DbSet<User> Users
		{
			get;
			set;
		}
	}
}


