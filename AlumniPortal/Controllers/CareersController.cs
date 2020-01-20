using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Models;
using AlumniPortal.Utilities;

namespace AlumniPortal.Controllers
{
    [Authorize]
    public class CareersController : BaseController
    {

        public ActionResult Index()
        {
            var model = new CareersViewModel
            {
                Careers = careerRepo.GetCareers().Take(3).ToList(),
                CareerArticles = newsRepo.GetCareerArticles().Take(3).ToList()
            };
                
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult CareersHome()
        {
            var model = careerRepo.GetCareers();
            return PartialView(model);
        }

        public ActionResult DeloitteCareers()
        {
            var model =
                RSSReader.GetRSSFeed(
                    "https://careers.deloitte.com/jobs/eng-ZA/rss/c/South-Africa/a/Experienced-Professionals").PickRandom(5);
            return PartialView(model);
        }
    }
}