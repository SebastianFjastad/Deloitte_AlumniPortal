using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Entities
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CoverImageId { get; set; }
        public Asset CoverImage { get; set; }
        public List<Asset> Images { get; set; }
    }
}