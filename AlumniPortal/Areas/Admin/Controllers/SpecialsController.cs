using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Areas.Admin.Models;
using AlumniPortal.Entities;
using AlumniPortal.Repositories;

namespace AlumniPortal.Areas.Admin.Controllers
{
    public class SpecialsController : Controller
    {
        private SpecialsRepository repo = new SpecialsRepository();

        public ActionResult Index()
        {
            var model = new SpecialViewModel
            {
                Specials = repo.GetSpecials()
            };
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Special special, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                repo.SaveSpecial(special, upload);
                return RedirectToAction("Index");
            }

            return View(special);
        }

        public ActionResult Edit(int id)
        {
            var model = new SpecialViewModel
            {
                Special = repo.GetSpecial(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Special special, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                repo.EditSpecial(special, upload);
                return RedirectToAction("Index");
            }

            var model = new SpecialViewModel
            {
                Special = special
            };

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var result = repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}