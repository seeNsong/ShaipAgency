using Microsoft.AspNetCore.Identity;

namespace ShaipAgency.Model
{
    public class ApplicationUser : IdentityUser
    {        
        public string ShaipName { get; set; }
    }
}
