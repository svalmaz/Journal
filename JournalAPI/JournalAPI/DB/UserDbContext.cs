using JournalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.DB
{
	public class UserDbContext : DbContext
	{
		public UserDbContext()
		{
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=user.db");
		}
		public DbSet<User> Users => Set<User>();
		public DbSet<Option> Options => Set<Option>();
		public DbSet<Entry> Entries => Set<Entry>();
	}
}
