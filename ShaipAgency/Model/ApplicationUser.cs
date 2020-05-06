using Microsoft.AspNetCore.Identity;
using ShaipAgency.Model.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShaipAgency.Model
{
    public class ApplicationUser : IdentityUser<int>
    {        
        [Column("ShaipName",TypeName ="Nvarchar(20)")]
        public string ShaipName { get; set; }
        public bool IsWithdraw { get; set; }


        public ICollection<UserActivityModel> UserActivityModel { get; set; }
        public ICollection<UserPassbookModel> UserPassbookModel { get; set; }


    }
}
