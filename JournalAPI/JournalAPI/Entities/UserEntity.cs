namespace JournalAPI.Entities
{
	public class UserEntity
	{
		public Guid Id { get; set; }
		public string Password { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public List<EntryEntity> Entries { get; set; } = [];
	}
}
