using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Entities;

namespace AlumniPortal.Models
{
    public class GalleryViewModel
    {
        public GalleryViewModel()
        {
            Albums = new List<Album>();
        }

        public List<Album> Albums { get; set; } 
    }
}