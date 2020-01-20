using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AlumniPortal.Entities
{
    public class Event
    {
        public Event()
        {
            EventInvites = new List<EventInvite>();
        }

        public int EventID { get; set; }
        
        [Required(ErrorMessage = "Please add a title")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Please add description")]
        public string Description { get; set; }
        
        [Display(Name = "Location name")]
        [Required(ErrorMessage = "Please add location name")]
        public string LocationName { get; set; }

        [Required(ErrorMessage = "Event must have a Region")]
        public string Region { get; set; }
        
        public decimal? Lattitude { get; set; }
        public decimal? Longitude { get; set; }
        
        [Required(ErrorMessage = "Please select start time")]
        public DateTime From { get; set; }
        
        [Required(ErrorMessage = "Please select end time")]
        public DateTime To { get; set; }

        [Display(Name = "Contact number")]
        public string ContactNumber { get; set; }
        public virtual ICollection<EventInvite> EventInvites { get; set; }
    }
}