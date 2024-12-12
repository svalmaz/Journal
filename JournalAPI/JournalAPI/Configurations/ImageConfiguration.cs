using JournalAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JournalAPI.Configurations
{
	public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
	{
		public void Configure(EntityTypeBuilder<ImageEntity> builder)
		{
			builder.HasOne(i => i.Entry)
				.WithMany(e => e.Images)
				.HasForeignKey(e => e.EntryId);

		}
	}
}

