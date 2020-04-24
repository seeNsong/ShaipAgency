using Microsoft.AspNetCore.Identity;

namespace ShaipAgency.Model
{
    public class ApplicationUser : IdentityUser<int>
    {        
        public string ShaipName { get; set; }
        public bool IsWithdraw { get; set; }
    }
}
