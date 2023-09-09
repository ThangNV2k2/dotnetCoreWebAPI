using Microsoft.AspNetCore.Identity;

namespace MyApiNet6.data
{
    public class ApplicationUser : IdentityUser
    {
        public String FristName { get; set; } = null!;
        public String LastName { get; set; } = null!;

    }
}
