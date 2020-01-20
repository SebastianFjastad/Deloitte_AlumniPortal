using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using AlumniPortal.Models;

namespace AlumniPortal.Entities
{
    public class Asset
    {
        public int AssetId { get; set; }
        [StringLength(255)]
        public string AssetName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public FileType FileType { get; set; }
        public int? ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? AlumnusId { get; set; }
        public virtual Alumnus Alumnus { get; set; }
        public int? AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}