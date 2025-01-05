public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
}

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IList<string> Roles { get; set; }

    public string About { get; set; }

    public string ProfilePicture { get; set; }
}
public class LoginResponseDto
{
    public string Token { get; set; }
    public UserDto User { get; set; }
}