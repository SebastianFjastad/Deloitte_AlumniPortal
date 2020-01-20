using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Models;

namespace AlumniPortal.Entities
{
    public class Alumnus
    {
        [Key]
        public int AlumnusID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string Website { get; set; }
        [AllowHtml]
        public string About { get; set; }
        [AllowHtml]
        public string Interview { get; set; }
        public string ProfileLink { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Asset Asset { get; set; }
    }
}