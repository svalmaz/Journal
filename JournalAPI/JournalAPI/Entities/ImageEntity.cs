namespace JournalAPI.Entities
{
	public class ImageEntity
	{
		public Guid Id { get; set; }
		public string Path { get; set; }= string.Empty;
		public Guid EntryId { get; set; }
		public EntryEntity? Entry { get; set; }
	}
}
