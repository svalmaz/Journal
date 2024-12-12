using JournalAPI.Entities;
using JournalAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JournalAPI.Services.Auth
{
	public class JwtProvider
	{
		public readonly JwtOptions _options;
		public JwtProvider(IOptions<JwtOptions> options)
		{
			_options = options.Value;
		}
		public string GenerateToken(UserEntity user)
		{
			string secr = "justsecretkeyformyjournalapiproject";
			Claim[] claims = [new("userId", user.Id.ToString())];
			var signingCredantials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secr)),
				SecurityAlgorithms.HmacSha256
				);
			var token = new JwtSecurityToken(
				claims: claims,
				signingCredentials: signingCredantials,
				expires: DateTime.UtcNow.AddHours(_options.ExpitesHours)
				);
			var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenValue;
		

		}
	}
}
