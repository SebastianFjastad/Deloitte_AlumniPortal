using System.Collections.Generic;
using AlumniPortal.Entities;
using AlumniPortal.Models;
using AlumniPortal.Utilities;

namespace AlumniPortal.Areas.Admin.Models
{
    public class EventViewModel : ViewModelBase
    {
        public EventViewModel()
        {
            Event = new Event();
            Events = new List<Event>();
            Users = new List<ApplicationUser>();
            Email = new EmailDto();
        }

        public int Attending { get; set; }
        public int EventId { get; set; }

        public Event Event { get; set; }

        public List<ApplicationUser> Users { get; set; } 

        public List<Event> Events { get; set; }

        public EmailDto Email { get; set; }
    }
}