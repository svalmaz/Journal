using JournalAPI.Interfaces.Hash;
using JournalAPI.Models;
using System.Collections;

namespace JournalAPI.Services.Auth
{
	public class UserService
	{
		private readonly IPasswordHasher _passwordHasher;
		public UserService(IPasswordHasher passwordHasher)
		{
			_passwordHasher = passwordHasher;
		}
		public async Task Register(UserDto user)
		{
			var hashedPassword = _passwordHasher.Generate(user.Password);
			 
		}
	}
}
