namespace JournalAPI.Models
{
	public class EntryDto
	{
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public List<string> Images { get; set; } = new List<string>();
	}
}
