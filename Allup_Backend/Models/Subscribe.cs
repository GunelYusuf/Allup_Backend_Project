using System;
using System.ComponentModel.DataAnnotations;

namespace Allup_Backend.Models
{
    public class Subscribe
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
