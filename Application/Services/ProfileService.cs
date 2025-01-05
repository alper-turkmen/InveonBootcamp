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
        user.About = profileDto.About;

        return await _userManager.UpdateAsync(user);
    }   

    public async Task<IList<string>> GetRolesByUserIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null;
        }

        return await _userManager.GetRolesAsync(user);
    }

    public async Task<IdentityResult> UpdateProfilePictureAsync(string userId, string fileName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        user.ProfilePicture = fileName;

        return await _userManager.UpdateAsync(user);
    }
}
