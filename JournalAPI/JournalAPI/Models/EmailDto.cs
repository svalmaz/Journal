using System.ComponentModel.DataAnnotations;

namespace JournalAPI.Models
{
	public class EmailDto
	{
		[Required]
		[EmailAddress]
		public string RecipientEmail { get; set; }

		[Required]
		public string Subject { get; set; }

		[Required]
		public string Body { get; set; }
	}
}
