using Microsoft.AspNetCore.Identity;

namespace Business.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; } = null!;
    }
}
