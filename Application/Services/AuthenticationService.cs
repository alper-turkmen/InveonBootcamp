using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


public class AuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !(await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false)).Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid login attempt.");
        }


    var token = await GenerateJwtToken(user);

    return new LoginResponseDto
    {
        Token = token,
        User = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.Name,
            LastName = user.Surname,
            About = user.About,
            ProfilePicture = user.ProfilePicture,
            Roles = await _userManager.GetRolesAsync(user)
        }
    };


    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByNameAsync(registerDto.Username);
        if (existingUser != null)
        {
            throw new Exception("Username is already taken.");
        }

        var user = new User
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            EmailConfirmed = true,
            Name = registerDto.Name,
            Surname = registerDto.Surname
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            throw new Exception("User registration failed.");
        }

        await _userManager.AddToRoleAsync(user, "User");
        return "User registered successfully!";
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}