using JournalAPI.DB;
using JournalAPI.Entities;
using JournalAPI.Interfaces.Hash;
using JournalAPI.Models;
using JournalAPI.Services.Hashing;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.Repositories
{
	
	public class UserRepository
	{
		public readonly JournalDbContext _dbContext;
		public readonly IPasswordHasher _passwordHasher;
		public UserRepository(JournalDbContext dbContext, IPasswordHasher passwordHasher)
		{
			_dbContext = dbContext;
			_passwordHasher = passwordHasher;
		}
		// Регистрация пользователя через UserDto
		public async Task<string> AddUser(UserDto user)
		{
		
			var u = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email ==  user.Email);
			var hashedPass = _passwordHasher.Generate(user.Password);
			if (u == null)
			{
				var newUser = new UserEntity
				{
					Email = user.Email,
					Password = hashedPass
				};
				_dbContext.Users.Add(newUser);
				await _dbContext.SaveChangesAsync();
				return "Success";

			}
			else
			{
				return "Email already exist";
			}
		}


		//Вход
		//public string GetUser(UserDto user)
		//{
		//	var u = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
		//	if (u != null)
		//	{
		//		if (_hashingService.Verify(user.Password, u.Password))
		//		{
		//			Console.WriteLine("true");
		//			return ("Success");
		//		}
		//		else
		//		{
		//			return (" incorrect Pass");
		//		}
		//	}
		//	else
		//	{
		//		return ("email doesn't exist");
		//	}
		//}

	}
}
