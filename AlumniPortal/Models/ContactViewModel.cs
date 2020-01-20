using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlumniPortal.Models
{
    public class ContactViewModel
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public string SenderEmail { get; set; }
    }
}