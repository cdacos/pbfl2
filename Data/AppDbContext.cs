﻿using Microsoft.EntityFrameworkCore;
using Pbfl.Data.Models;

namespace Pbfl.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Error> Errors { get; set; } = default!;
		public DbSet<Login> Logins { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.RemovePluralizingTableNameConvention();
		}

		public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }
	}
}
