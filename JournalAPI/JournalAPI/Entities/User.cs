using System.ComponentModel.DataAnnotations;

namespace JournalAPI.Entities
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string userName { get; set; }
		public string userPass { get; set; }
		public string userEmail { get; set; }
	}
	public class Entry
	{
		[Key]
		public int Id { get; set; }
		public int userId { get; set; }
		public string text { get; set; }
	}
	public class Option
	{
		[Key]
		public int Id { get; set; }
		public int entriesId { get; set; }
		public string options { get; set; }

	}
}
