using System.Collections.Generic;
using AlumniPortal.Entities;

namespace AlumniPortal.Areas.Admin.Models
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            Article = new Article();
            Articles = new List<Article>();
            Video = new Video();
        }

        public Article Article { get; set; }
        public List<Article> Articles { get; set; }
        public Video Video { get; set; }
    }
}