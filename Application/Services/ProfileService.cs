using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class ProfileService
{
    private readonly UserManager<User> _userManager;

    public ProfileService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<IdentityResult> UpdateProfileAsync(string userId, UpdateProfileDto profileDto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        user.Name = profileDto.Name;
        user.Surname = profileDto.Surname;

        return await _userManager.UpdateAsync(user);
    }
}