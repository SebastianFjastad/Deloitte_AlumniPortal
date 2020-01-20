using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Areas.Admin.Models;
using AlumniPortal.Models;
using AlumniPortal.Repositories;

namespace AlumniPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, ContentAdmin")]
    public class UsersController : Controller
    {
        private UserRepository userRepo = new UserRepository();

        public ActionResult Index()
        {
            var model = new UserViewModel
            {
                Users = userRepo.GetUsers()
            };
            return View(model);
        }

        //public ActionResult Details(string id)
        //{
        //    ApplicationUser applicationUser = userRepo.GetUser(id);
        //    return View(applicationUser);
        //}

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        userRepo.ApplicationUsers.Add(applicationUser);
        //        userRepo.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(applicationUser);
        //}

        public ActionResult Edit(string id)
        {
            var model = userRepo.GetUser(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                var model = userRepo.SaveUser(applicationUser);
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        public ActionResult Delete(string id)
        {
            var model = new UserViewModel
            {
                User = userRepo.GetUser(id)
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var result = userRepo.SoftDeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}