using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; } // Ek alan
        public string Surname { get; set; } // Ek alan
    }
}