using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; } 
        public string Surname { get; set; }

        public string About { get; set; }

        public string ProfilePicture { get; set; } 

        public List<Order> Orders { get; set; } 
    }
}