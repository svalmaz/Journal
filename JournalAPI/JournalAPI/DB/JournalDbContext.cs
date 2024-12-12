using JournalAPI.Configurations;
using JournalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.DB
{
	public class JournalDbContext : DbContext
	{

		public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options) {

			Database.EnsureCreated();
		}

		public DbSet<UserEntity> Users {  get; set; }
		public DbSet<EntryEntity> Entries { get; set; }
		public DbSet<ImageEntity> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new EntryConfiguration());
			builder.ApplyConfiguration(new ImageConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());
			base.OnModelCreating(builder);
		}
	}
}
