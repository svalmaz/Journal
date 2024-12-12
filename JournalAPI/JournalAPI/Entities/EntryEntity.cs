namespace JournalAPI.Entities
{
	public class EntryEntity
	{
		public Guid Id {  get; set; }
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime LastEditedDate { get; set; } = DateTime.UtcNow;
		public Guid UserId { get; set; } 
		public UserEntity? User { get; set; }
		public List<ImageEntity> Images { get; set; } = [];
		
	}
}
