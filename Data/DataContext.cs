using System;
using CloudDevAPI_DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudDevAPI_DotNet.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		//public DbSet<User> Users { get; set; }
		public DbSet<Country> Country { get; set; }
		//public DbSet<Continent> Continents { get; set; }
		//public DbSet<Friendship> Friendships { get; set; }

	}
}

