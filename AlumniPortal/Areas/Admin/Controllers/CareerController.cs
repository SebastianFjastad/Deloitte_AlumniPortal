using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Areas.Admin.Models;
using AlumniPortal.Controllers;
using AlumniPortal.Entities;
using AlumniPortal.Repositories;

namespace AlumniPortal.Areas.Admin.Controllers
{
    public class CareerController : Controller
    {
        private CareerRepository careerRepo = new CareerRepository();
        
        public ActionResult Index()
        {
            var model = careerRepo.GetCareers();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Career career)
        {
            if (ModelState.IsValid)
            {
                careerRepo.SaveCareer(career);
                return RedirectToAction("Index");
            }

            return View(career);
        }

        public ActionResult Edit(int id)
        {
            var model = careerRepo.GetCareer(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Career career)
        {
            if (ModelState.IsValid)
            {
                careerRepo.EditCareer(career);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", career);

        }

        public ActionResult Delete(int id)
        {
            var result = careerRepo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}