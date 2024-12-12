using Azure.Core.Pipeline;

namespace JournalAPI.Services.Auth
{
	public class JwtOptions
	{
		public string SecretKey { get; set; } = String.Empty;
		public int ExpitesHours { get; set; } = 1;
	}
}
