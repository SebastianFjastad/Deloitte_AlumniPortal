using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Areas.Admin.Models;
using AlumniPortal.Entities;
using AlumniPortal.Models;
using AlumniPortal.Repositories;

namespace AlumniPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, ContentAdmin")]
    public class NewsController : Controller
    {
        private NewsRepository newsRepo = new NewsRepository();

        #region News Articles
        public ActionResult Index()
        {
            var model = new NewsViewModel
            {
                Articles = newsRepo.GetArticles(),
                Video = newsRepo.GetVideo()
            };
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                newsRepo.SaveArticle(article, upload);
                return RedirectToAction("Index");
            }

            return View(article);
        }

        public ActionResult Edit(int id)
        {
            var model = new NewsViewModel
            {
                Article = newsRepo.GetArticle(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                newsRepo.EditArticle(article, upload);
                return RedirectToAction("Index");
            }

            var model = new NewsViewModel
            {
                Article = article
            };

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var result = newsRepo.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion

        #region Youtube Vid

        public ActionResult SaveVideo(NewsViewModel model)
        {
            newsRepo.SaveVideo(model.Video);
            return RedirectToAction("Index");
        }
        #endregion
    }
}