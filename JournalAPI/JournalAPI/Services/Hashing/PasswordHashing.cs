using JournalAPI.Interfaces.Hash;

namespace JournalAPI.Services.Hashing
{
	public class PasswordHashing : IPasswordHasher
	{
		public string Generate(string password) =>
			BCrypt.Net.BCrypt.EnhancedHashPassword(password);

		public bool Verify(string password, string hashedPassword) =>
			BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
	}
}
