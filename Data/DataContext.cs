﻿using System;
using CloudDevAPI_DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudDevAPI_DotNet.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			//modelBuilder.Entity<UserCountry>()
			//.HasKey(p => new { p.UserId, p.CountryId });

			modelBuilder.Entity<User>()
				.HasMany(e => e.Country)
				.WithMany(e => e.User)
				.UsingEntity("UserCountry");

            base.OnModelCreating(modelBuilder);
        }

		public DbSet<User> User { get; set; }
		public DbSet<Country> Country { get; set; }
		public DbSet<Continent> Continent { get; set; }
		//public DbSet<Friendship> Friendship { get; set; }

	}
}

