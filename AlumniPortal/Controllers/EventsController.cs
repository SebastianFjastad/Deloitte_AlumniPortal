using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Mvc;
using AlumniPortal.Entities;
using AlumniPortal.Models;
using Microsoft.AspNet.Identity;

namespace AlumniPortal.Controllers
{
    [Authorize]
    public class EventsController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Calendar(bool isVertical = false)
        {
            var model = new CalendarViewModel
            {
                Events = eventRepo.GetEvents(),
                User = userRepo.GetUser(User.Identity.GetUserId())
            };

            return isVertical ? PartialView("CalendarVertical", model) : PartialView(model);
        }

        public ActionResult AttendEvent(string userId, int eventId, bool isApplication)
        {
            var result = eventRepo.UserAttendEvent(userId, eventId, isApplication);

            if (result)
            {
                return Json(new { Success = "False", Message = "Could not complete application" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = "True", Message = "Thank you for applying" }, JsonRequestBehavior.AllowGet);
        }
    }
}
