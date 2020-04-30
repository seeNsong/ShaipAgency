

using System;
using System.ComponentModel.DataAnnotations;

namespace ShaipAgency.Model.Standards
{
    public class RequestCodeModel
    {
        [Key]
        public string RequestCode { get; set; }
        [Required]
        public string RequestName { get; set; }
        
        public string UseYN { get; set; }

        public int UserId { get; set; }

        public DateTime CreationDateTime { get; set; }
    }
}
