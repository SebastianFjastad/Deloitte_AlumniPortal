using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Entities;

namespace AlumniPortal.Areas.Admin.Models
{
    public class SpecialViewModel
    {
        public SpecialViewModel()
        {
            Special = new Special();
            Specials = new List<Special>();
        }

        public Special Special { get; set; }

        public List<Special> Specials { get; set; } 
    }
}