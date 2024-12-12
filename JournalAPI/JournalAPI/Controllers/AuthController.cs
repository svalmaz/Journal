using Azure.Core;
using JournalAPI.DB;
using JournalAPI.Entities;
using JournalAPI.Interfaces.Email;
using JournalAPI.Interfaces.Hash;
using JournalAPI.Models;
using JournalAPI.Repositories;
using JournalAPI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		public readonly JournalDbContext _dbContext;
		public readonly IPasswordHasher _passwordHasher;
		public readonly JwtProvider _jwtProvider;
		public readonly IHttpContextAccessor  _httpContext;
		private readonly IEmailService _emailService;
		public AuthController(IEmailService emailService, IHttpContextAccessor  httpContext, JournalDbContext journalDbContext, IPasswordHasher passwordHasher, JwtProvider jwtProvider)
		{
			_emailService = emailService;
			_dbContext = journalDbContext;
			_passwordHasher = passwordHasher;
			_jwtProvider = jwtProvider;
			_httpContext = httpContext;
		}
		[HttpPost("register")]
		public async Task<ActionResult> Register(UserDto user)
		{
			if (_dbContext.Users == null)
			{
				return Problem("Oops... Something happened to the database");
			}

			if (_dbContext.Users.Count(u => u.Email == user.Email) > 0)
			{
				return Ok(new ApiResponse { Status = "failed", Message = "user with this email is already registered." });
			}

			else
			{

				string hashPass = _passwordHasher.Generate(user.Password);

				var new_user = new UserEntity()
				{
					Email = user.Email,
					Password = hashPass,


				};

				_dbContext.Users.Add(new_user);
				await _dbContext.SaveChangesAsync();

				return Ok(new ApiResponse { Status = "Success", Message = "Success" });



			}


		}
		[Authorize]
		[HttpGet("asd")]
		public async Task<ActionResult<string>> getData()
		{
			return "Good Job";
		}
		[HttpPost("resetPass")]
		public async Task<ActionResult<String>> resetPass(ResetPasswordDto email)
		{
			if (_dbContext.Users == null)
			{
				return Problem("Oops... Something happened to the database");
			}
			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email.Email);
			if (user == null)
			{
				return NotFound("User with the provided email does not exist.");
			}
			string newPassword = GenerateRandomPassword(8);

			
			string hashedPassword = _passwordHasher.Generate(newPassword);
			user.Password = hashedPassword;
			await _dbContext.SaveChangesAsync();
			_emailService.SendEmail(email.Email, "New Pass", $"New pass: {newPassword}");

			return Ok(new ApiResponse
			{
				Status = "Success",
				Message = "Password has been reset successfully.",
			
			});
		}
		private string GenerateRandomPassword(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserDto user)
		{
			if (_dbContext.Users == null)
			{
				return Problem("Oops... Something happened to the database");
			}

			if (_dbContext.Users.Count(u => u.Email == user.Email) > 0)
			{
				var u = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
				var result = _passwordHasher.Verify(user.Password, u.Password);
				if (!result)
				{
					return Problem("Wrong password");
				}
				else
				{
					var jwtToken = _jwtProvider.GenerateToken(u);
					_httpContext.HttpContext.Response.Cookies.Append("jwt", jwtToken);
					return jwtToken;
				}
			}


			else
			{

				return Problem("User not found");


			}


		}
	}
}
