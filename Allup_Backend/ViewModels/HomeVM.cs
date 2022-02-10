using System;
using System.Collections.Generic;
using Allup_Backend.Models;

namespace Allup_Backend.ViewModels
{
    public class HomeVM
    {
        public List<AuthorSlider> AuthorSliders { get; set; }

        public List<Category> categories { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
