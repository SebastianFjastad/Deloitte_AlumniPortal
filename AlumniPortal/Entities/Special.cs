using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlumniPortal.Entities
{
    public class Special
    {
        public int SpecialId { get; set; }
        [Required]
        public string Title { get; set; }
        [AllowHtml]
        [Required]
        public string Body { get; set; }
        public Asset Image { get; set; }
        [Required]
        [Display(Name = "Link to special")]
        public string SpecialUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}