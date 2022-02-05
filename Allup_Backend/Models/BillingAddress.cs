using System;
namespace Allup_Backend.Models
{
    public class BillingAddress
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public AppUser User { get; set; }

        public string  UserId { get; set; }
    }
}
