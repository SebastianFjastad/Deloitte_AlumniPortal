using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AlumniPortal.Entities;

namespace AlumniPortal.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        public ActionResult Index()
        {
            var model = newsRepo.GetNewsArticles().Take(4).ToList();
            return View(model);
        }

        public ActionResult GetArticlesPage(int pageNo = 1, int pageSize = 4)
        {
            List<Article> model =
                newsRepo.GetNewsArticles()
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

            return PartialView(model);
        }
       
        public ActionResult Article(int id)
        {
            var model = newsRepo.GetArticle(id);
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult HomepageArticles()
        {
            var model = newsRepo.GetNewsArticles();
            return PartialView(model);
        }

        [AllowAnonymous]
        public ActionResult YoutubeVid()
        {
            var model = newsRepo.GetVideo();
            return PartialView(model);
        }
    }
}
