using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Entities;

namespace AlumniPortal.Models
{
    public class AlumniViewModel
    {
        public AlumniViewModel()
        {
            Alumni = new List<Alumnus>();
            Users = new List<ApplicationUser>();
        }

        public List<Alumnus> Alumni { get; set; } 
        public List<ApplicationUser> Users { get; set; } 
    }
}