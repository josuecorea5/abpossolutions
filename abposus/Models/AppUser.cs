using Microsoft.AspNetCore.Identity;

namespace abposus.Models
{
    public class AppUser : IdentityUser
    {
        public Roles Type { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
