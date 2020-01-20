using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Entities;

namespace AlumniPortal.Models
{
    public class CalendarViewModel
    {
        public CalendarViewModel()
        {
            Events = new List<Event>();
            User = new ApplicationUser();
        }

        public ApplicationUser User { get; set; }
        public List<Event> Events { get; set; }  
    }
}