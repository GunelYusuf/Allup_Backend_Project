using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Allup_Backend.Models
{
    public class AppUser:IdentityUser
    {
        [Required, StringLength(maximumLength: 50)]

        public string FullName { get; set; }


        public bool IsActive { get; set; }

        public bool Subscribe { get; set; }

        //public List<Sales> Sales { get; set; }
    }
}
