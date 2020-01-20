using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Entities;
using AlumniPortal.Repositories;

namespace AlumniPortal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, ContentAdmin")]
    public class GalleryController : Controller
    {
        private GalleryRepository repo = new GalleryRepository();

        public ActionResult Index()
        {
            var model = repo.GetAlbums();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var result = repo.CreateAlbum(model, upload);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public JsonResult SaveImages(int? id)
        {
            if (id != null)
            {
                List<HttpPostedFileBase> images = (from string name in Request.Files select Request.Files[name]).ToList();
                var result = repo.SaveImages(id, images);
                if (!result)
                {
                    return Json(new { Message = "Uploaded successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Message = "Failed to upload files" }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult LoadImages(int id)
        {
            var images = repo.GetImages(id);
            return PartialView("_ImageGallery", images);
        }

        public ActionResult Edit(int id)
        {
            var model = repo.GetAlbum(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var result = repo.EditAlbum(model, upload);
                return RedirectToAction("Edit", result.AlbumId);
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var result = repo.DeleteAlbum(id);
            return RedirectToAction("Index");
        }

        public JsonResult DeleteImage(int id)
        {
            var result = repo.DeleteImage(id);
            var response = new { Success = "True", Message = "Deleted" };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
