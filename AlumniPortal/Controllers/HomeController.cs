using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.DbContext;
using AlumniPortal.Models;
using AlumniPortal.Repositories;
using AlumniPortal.Utilities;
using Microsoft.AspNet.Identity;

namespace AlumniPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserRepository userRepo = new UserRepository();

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("ContentAdmin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            var sender = userRepo.GetUser(User.Identity.GetUserId());

            //EmailSender.SendEmail(new EmailDto
            //{
            //    Subject = model.Subject,
            //    Body = model.Body, 
            //    Sender = sender.Email,
            //    IncomingEmail = true
            //});

            TempData["EmailSent"] = true;
            return RedirectToAction("Contact");
        }

        [AllowAnonymous]
        public ActionResult BlogPosts()
        {
            var model = RSSReader.GetRSSFeed("http://deloitteblog.co.za/feed/").PickRandom(3);
            return PartialView(model);
        }
    }
}