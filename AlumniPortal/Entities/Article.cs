using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Models;

namespace AlumniPortal.Entities
{
    public class Article
    {
        public int ArticleID { get; set; }
        [Required(ErrorMessage = "Title required")]
        public ArticleType ArticleType { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Article body required")]
        public string Body { get; set; }
        [Display(Name = "Number of times read")]
        public int? NoTimesRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}