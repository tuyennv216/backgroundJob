﻿using backgroundJob.Custom.ProcessTracking.Entity;
using Microsoft.EntityFrameworkCore;

namespace backgroundJob.Custom.ProcessTracking.Database
{
	public class ProcessContext : DbContext
	{
		private readonly string connectionStr = @"Server=.\SQLEXPRESS;Database=BackgroundJob;Integrated Security=true;TrustServerCertificate=True;";
		private readonly string migrationTable = @"__MigrationsHistory";
		private readonly string schema = "PT";

		public ProcessContext() { }

		public ProcessContext(DbContextOptions<ProcessContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(schema);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(connectionStr, d => d.MigrationsHistoryTable(migrationTable, schema));
		}

		public DbSet<PTProcess> Process { get; set; }
		public DbSet<PTTime> Time { get; set; }
	}
}
