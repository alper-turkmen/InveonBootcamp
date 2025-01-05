using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
    private readonly IWebHostEnvironment _environment;
        private readonly ProfileService _profileService;

        public AccountController(IWebHostEnvironment environment,ProfileService profileService)
        {
            _environment = environment;
            _profileService = profileService;
        }

        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto profileDto)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found.");
            }

            var result = await _profileService.UpdateProfileAsync(userId, profileDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

 



[HttpPost("profile/picture")]
public async Task<IActionResult> UpdateProfilePicture([FromBody] FileUploadModel model)
{
    if (model == null || string.IsNullOrEmpty(model.FileBase64) || string.IsNullOrEmpty(model.FileName))
    {
        return BadRequest("Invalid input data.");
    }

    try
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "files/profiles");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var bytes = Convert.FromBase64String(model.FileBase64);
        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.FileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await System.IO.File.WriteAllBytesAsync(filePath, bytes);

        var fileUrl = $"/files/profiles/{uniqueFileName}";
        
        var userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found.");
        }

        var result = await _profileService.UpdateProfilePictureAsync(userId, fileUrl);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { fileUrl });
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}




        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found.");
            }

            var user = await _profileService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roles = await _profileService.GetRolesByUserIdAsync(userId);

            return Ok(new
            {
                user.Name,
                user.Surname,
                user.Email,
                user.PhoneNumber,
                roles                
            });
        }
    }
}