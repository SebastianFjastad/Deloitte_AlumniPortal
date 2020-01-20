using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Models;
using AlumniPortal.Utilities;
using Microsoft.AspNet.Identity;

namespace AlumniPortal.Controllers
{
    [Authorize]
    public class AlumniController : BaseController
    {
        public ActionResult Index()
        {
            //get max three random featured alumni, 6 total including users
            var alumni = alumniRepo.GetRandomAlumni(3);
            var numUsersToGet = (6 - alumni.Count);
            var idList = new List<string> { User.Identity.GetUserId() };
            var users = userRepo.GetRandomUsers(numUsersToGet, idList);

            var model = new AlumniViewModel
            {
                Alumni = alumni,
                Users = users
            };
            return View(model);
        }

        public ActionResult Alumnus(string userId = "", int? alumnusId = null, bool isFeatured = false)
        {
            if (!isFeatured && User.Identity.GetUserId() != userId)
            {
                var model = new AlumnusViewModel
                {
                    User = userRepo.GetUser(userId),
                    IsFeatured = false,
                    ConversationStatus = myConnectRepo.GetConnectionStatus(User.Identity.GetUserId(), userId)
                };
                return View(model);
            }
            if (userId == User.Identity.GetUserId())
            {
                return RedirectToAction("Profile");
            }
            else
            {
                var model = new AlumnusViewModel
                {
                    Alumnus = alumniRepo.GetAlumnus(alumnusId ?? 0),
                    IsFeatured = true,
                };
                return View(model);
            }
        }

        public new ActionResult Profile()
        {
            var model = userRepo.GetUser(User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult UpdateProfile(ApplicationUser user, string croppedImage)
        {
            
            var result = alumniRepo.EditProfile(user, croppedImage);
            return RedirectToAction("Alumnus", new { userId = user.Id });
        }

        public ActionResult GetUserPage(string[] ids)
        {
            var idList = ids.ToList();
            idList.Add(User.Identity.GetUserId());
            var model = new AlumniViewModel { Users = userRepo.GetRandomUsers(6, idList) };
            return PartialView(model);
        }

        public ActionResult SearchAlumni(string term)
        {
            var result = alumniRepo.SearchAlumni(term);
            var model = result.Select(r => new { label = r.FirstName + " " + r.LastName, value = r.Id }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeactivateAccount()
        {
             var result = userRepo.SoftDeleteUser(User.Identity.GetUserId());
             return Json(new { HasError = result, Message = "We are sorry to see you go!" }, JsonRequestBehavior.AllowGet);
        }
    }
}