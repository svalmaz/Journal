using JournalAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JournalAPI.Configurations
{
	public class EntryConfiguration : IEntityTypeConfiguration<EntryEntity>
	{
		public void Configure(EntityTypeBuilder<EntryEntity> builder)
		{
			builder.HasKey(i => i.Id);
			builder.HasOne(e => e.User)
				.WithMany(u => u.Entries)
				.HasForeignKey(u => u.UserId);

			builder.HasMany(i => i.Images)
				.WithOne(e => e.Entry);

		}
		
	}

}
