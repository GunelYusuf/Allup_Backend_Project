using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Allup_Backend.Models
{
    public class AppUser:IdentityUser
    {
        [Required, StringLength(maximumLength: 50)]

        public string FullName { get; set; }
    }
}
