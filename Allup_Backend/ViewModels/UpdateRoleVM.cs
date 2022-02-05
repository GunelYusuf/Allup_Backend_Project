using System;
using System.Collections.Generic;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Allup_Backend.ViewModels
{
    public class UpdateRoleVM
    {
        public IList<IdentityRole> Roles { get; set; }

        public IList<string> UserRoles { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }
    }
}
