using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Entities;
using AlumniPortal.Models;
using Microsoft.AspNet.Identity;

namespace AlumniPortal.Controllers
{
    [Authorize]
    public class MyConnectController : BaseController
    {
        public ActionResult Index()
        {
            var model = myConnectRepo.GetConversations(User.Identity.GetUserId())
                .OrderByDescending(x => x.LastModified)
                .Take(5).ToList();
            return View(model);
        }

        public ActionResult GetConversationsPage(int pageNo = 1)
        {
            List<Conversation> model =
                myConnectRepo.GetConversations(User.Identity.GetUserId())
                .OrderByDescending(x => x.LastModified)
                    .Skip((pageNo - 1) * 4)
                    .Take(4)
                    .ToList();

            return PartialView(model);
        }

        public ActionResult Conversation(int convId)
        {
            var model = myConnectRepo.GetConversation(User.Identity.GetUserId(), convId);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult InitiateConnect(string userId)
        {
            var result = myConnectRepo.InitiateConversation(User.Identity.GetUserId(), userId);
            return Json(new { response = result ? "Connect request sent" : "Unable to connect"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateConnectStatus(int conversationId, string userId, bool isAccepted)
        {
            var result = myConnectRepo.UpdateConversationStatus(conversationId, userId, isAccepted);
            return Json(new
            {
                message = result ? "Start chatting" : "There was a problem hiding this conversation"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchAlumni(string term)
        {
            return new JsonResult();
        }
    }
}