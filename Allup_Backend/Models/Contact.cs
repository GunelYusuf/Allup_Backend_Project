using System;
using System.ComponentModel.DataAnnotations;

namespace Allup_Backend.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

       public string Address { get; set; }

        public string Description{ get; set; }

        public string MapUrl { get; set; }

        public string OpenCloseClocks { get; set; }
    }
}
