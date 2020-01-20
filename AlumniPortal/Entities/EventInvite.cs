using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AlumniPortal.Models;

namespace AlumniPortal.Entities
{
    public class EventInvite
    {
        [Key]
        public int EventInviteID { get; set; }

        public string Id { get; set; }
        public int EventID { get; set; }
        public bool Attending { get; set; }
        public bool IsApplication { get; set; }
        public Event Event { get; set; }
        public ApplicationUser User { get; set; }
    }
}