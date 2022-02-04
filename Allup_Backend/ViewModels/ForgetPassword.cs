using System;
using System.ComponentModel.DataAnnotations;
using Allup_Backend.Models;

namespace Allup_Backend.ViewModels
{
    public class ForgetPassword
    {
        public AppUser User { get; set; }

        public string Token { get; set; }

        [DataType(DataType.Password)]

        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
