using System;
using System.Collections.Generic;

namespace Allup_Backend.Models
{
    public class Color
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ProductColor> ProductColors { get; set; }
    }
}
