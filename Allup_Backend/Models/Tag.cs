using System;
using System.Collections.Generic;

namespace Allup_Backend.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
