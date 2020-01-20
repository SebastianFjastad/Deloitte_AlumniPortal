using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Entities;

namespace AlumniPortal.Models
{
    public class CareersViewModel
    {
        public CareersViewModel()
        {
            Careers = new List<Career>();
            CareerArticles = new List<Article>();
        }

        public List<Article> CareerArticles { get; set; } 
        public List<Career> Careers { get; set; } 
    }
}