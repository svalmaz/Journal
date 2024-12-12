using JournalAPI.DB;
using JournalAPI.Entities;
using JournalAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EntriesController : ControllerBase
	{
		public readonly JournalDbContext _dbContext;
		public EntriesController(JournalDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		[HttpPost("add-entry")]
		[Authorize]
		public async Task<ActionResult> AddEntry([FromBody] EntryDto entryDto)
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID not found in JWT.");
			}

			Guid userId = Guid.Parse(userIdClaim.Value);

			string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
			if (!Directory.Exists(imagesFolder))
			{
				Directory.CreateDirectory(imagesFolder);
			}

			var newEntry = new EntryEntity
			{
				Id = Guid.NewGuid(),
				Title = entryDto.Title,
				Text = entryDto.Text,
				CreatedAt = DateTime.UtcNow,
				LastEditedDate = DateTime.UtcNow,
				UserId = userId,
				Images = new List<ImageEntity>()
			};

			foreach (var base64Image in entryDto.Images)
			{
				string imageName = $"{Guid.NewGuid()}.jpg";
				string imagePath = Path.Combine(imagesFolder, imageName);

				byte[] imageBytes = Convert.FromBase64String(base64Image);
				await System.IO.File.WriteAllBytesAsync(imagePath, imageBytes);

				newEntry.Images.Add(new ImageEntity
				{
					Id = Guid.NewGuid(),
					Path = imageName,
					EntryId = newEntry.Id
				});
			}

			_dbContext.Entries.Add(newEntry);
			await _dbContext.SaveChangesAsync();

			return Ok(new { Message = "Entry added successfully." });
		}
		private string GenerateImageLink(string imageName)
		{
			var request = HttpContext.Request;
			var baseUrl = $"{request.Scheme}://{request.Host}/images/";
			return $"{baseUrl}{imageName}";
		}
		[HttpGet("entries")]
		public async Task<ActionResult<IEnumerable<EntryGetDto>>> GetEntriesByUser()
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID not found in JWT.");
			}

			Guid userId = Guid.Parse(userIdClaim.Value);

			var userEntries = await _dbContext.Entries
				.Where(entry => entry.UserId == userId)
				.Include(entry => entry.Images)
				.ToListAsync();

			if (!userEntries.Any())
			{
				return NotFound("No entries found for the specified user.");
			}

			var result = userEntries.Select(entry => new EntryGetDto
			{
				Id= entry.Id,
				Title = entry.Title,
				Text = entry.Text,
				Images = entry.Images.Select(image => GenerateImageLink(image.Path)).ToList()
			});

			return Ok(result);
		}
		[HttpPut("entries/{id}")]
		public async Task<ActionResult> EditEntry(Guid id, [FromBody] EntryDto entryDto)
		{
			var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID not found in JWT.");
			}

			Guid userId = Guid.Parse(userIdClaim.Value);

			var entry = await _dbContext.Entries.FindAsync(id);

			if (entry == null)
			{
				return NotFound("Entry not found.");
			}

			if (entry.UserId != userId)
			{
				return Forbid("You are not allowed to edit this entry.");
			}

			entry.Title = entryDto.Title;
			entry.Text = entryDto.Text;
			entry.LastEditedDate = DateTime.UtcNow;

			_dbContext.Entries.Update(entry);
			await _dbContext.SaveChangesAsync();

			return Ok(new ApiResponse { Status = "Success", Message = "Entry updated successfully." });
		}
		[HttpDelete("entries/{entryIdS}/images/{imageIdS}")]
		public async Task<ActionResult> DeleteImage(string entryIdS, string imageIdS) { 
			Guid entryId  = Guid.Parse(entryIdS);
			Guid imageId = Guid.Parse(imageIdS);
		var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
			if (userIdClaim == null)
			{
				return Unauthorized("User ID not found in JWT.");
			}

			Guid userId = Guid.Parse(userIdClaim.Value);

			var entry = await _dbContext.Entries
				.Include(e => e.Images)
				.FirstOrDefaultAsync(e => e.Id == entryId);

			if (entry == null)
			{
				return NotFound("Entry not found.");
			}

			if (entry.UserId != userId)
			{
				return Forbid("You are not allowed to delete images from this entry.");
			}

			var image = entry.Images.FirstOrDefault(img => img.Id == imageId);
			if (image == null)
			{
				return NotFound("Image not found.");
			}

			var imagePath = Path.Combine("images", image.Path);
			if (System.IO.File.Exists(imagePath))
			{
				System.IO.File.Delete(imagePath);
			}

			_dbContext.Images.Remove(image);
			await _dbContext.SaveChangesAsync();

			return Ok(new ApiResponse { Status = "Success", Message = "Image deleted successfully." });
		}

	}



}
