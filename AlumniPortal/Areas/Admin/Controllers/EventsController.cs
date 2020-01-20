using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Areas.Admin.Models;
using AlumniPortal.Models;
using AlumniPortal.Repositories;
using AlumniPortal.Utilities;

namespace AlumniPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, ContentAdmin")]
    public class EventsController : Controller
    {
        private EventRepository eventRepo = new EventRepository();

        public ActionResult Index()
        {
            var model = new EventViewModel
            {
                Events = eventRepo.GetEvents()
            };

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = new EventViewModel
                {
                    HasErrors = eventRepo.SaveEvent(model.Event)
                };

                if (result.HasErrors)
                {
                    ViewBag.ErrorMessage = result.ErrorMessage;
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            var model = new EventViewModel
            {
                Event = eventRepo.GetEvent(id),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = new EventViewModel
                {
                    HasErrors = eventRepo.EditEvent(model.Event),
                };
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult EventApplications(int id)
        {
            var model = eventRepo.GetUserEventApplications(id);
            return PartialView(model);
        }

        public JsonResult AcceptApplication(List<string> userIds, int eventId)
        {
            var isError = eventRepo.AcceptApplications(userIds, eventId);

            return Json(new { success = !isError }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmailUsers(int eventId)
        {
            var model = new EventViewModel
            {
                Event = eventRepo.GetEvent(eventId),
                Users = eventRepo.GetUsersNotInEvent(eventId)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SendEmail(EventViewModel model)
        {
            var invites = eventRepo.GetEvent(model.EventId).EventInvites;

            //Render a view as email body TODO: add a view for the email body to e rendered as
            model.Email.Body = EmailBodyCreator.RenderViewToString("Events", "~/Views/Events/Email.cshtml", model);

            // 1 = attending
            if (model.Attending == 1)
            {
                var usersToEmail = invites.Where(i => i.Attending == true).Select(u => u.User);
                EmailSender.SendEmail(model.Email, usersToEmail);
            }
            // 2 = unresponded
            if (model.Attending == 2)
            {
                var usersToEmail = invites.Where(i => i.Attending == false && i.IsApplication == false).Select(u => u.User);
                EmailSender.SendEmail(model.Email, usersToEmail);
            }
            // 3 = applications
            if (model.Attending == 3)
            {
                var usersToEmail = invites.Where(i => i.Attending == false && i.IsApplication).Select(u => u.User);
                EmailSender.SendEmail(model.Email, usersToEmail);
            }
            return null;
        }

        public ActionResult Delete(int id)
        {
            var result = eventRepo.DeleteEvent(id);
            return RedirectToAction("Index");
        }
    }
}