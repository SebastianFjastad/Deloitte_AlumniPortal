using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Areas.Admin.Models;
using AlumniPortal.Repositories;
using Microsoft.AspNet.Identity;

namespace AlumniPortal.Areas.Admin.Controllers
{
    public class AlumniController : Controller
    {
        private AlumnusRepository repo = new AlumnusRepository();
        private UserRepository userRepo = new UserRepository();

        public ActionResult Index()
        {
            var model = repo.GetAlumni();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult FindUser(string term)
        {
            var result = userRepo.SearchUsers(term, User.Identity.GetUserId());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(AlumniViewModel model, HttpPostedFileBase upload)
        {
            try
            {
                var result = repo.SaveAlumnus(model.Alumnus, upload);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model = new AlumniViewModel
            {
                Alumnus = repo.GetAlumnus(id)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AlumniViewModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                repo.EditAlumnus(model.Alumnus, upload);
                return RedirectToAction("Index");
            }
            return null;
        }

        public ActionResult Delete(int id)
        {
            var result = repo.DeleteAlumnus(id);
            return RedirectToAction("Index");
        }
    }
}
