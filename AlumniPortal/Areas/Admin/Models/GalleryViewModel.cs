using System.Collections.Generic;
using AlumniPortal.Entities;

namespace AlumniPortal.Areas.Admin.Models
{
    public class GalleryViewModel
    {
        public GalleryViewModel()
        {
            Album = new Album();
            Albums = new List<Album>();
        }

        public Album Album { get; set; }
        public List<Album> Albums { get; set; }
    }
}