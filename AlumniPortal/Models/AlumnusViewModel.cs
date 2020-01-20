using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Entities;

namespace AlumniPortal.Models
{
    public class AlumnusViewModel
    {
        public AlumnusViewModel()
        {
            Alumnus = new Alumnus();
            User = new ApplicationUser();
        }

        public bool IsFeatured { get; set; }
        public ConversationStatus ConversationStatus { get; set; }

        public Alumnus Alumnus { get; set; }

        public ApplicationUser User { get; set; }
    }
}