using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Models;
using AlumniPortal.Utilities;

namespace AlumniPortal.Controllers
{
    [Authorize]
    public class GalleryController : BaseController
    {
        public ActionResult Index()
        {
            var model = new GalleryViewModel { Albums = galleryRepo.GetAlbums() };
            return View(model);
        }

        public ActionResult Album(int id)
        {
            var model = galleryRepo.GetAlbum(id);
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult GalleryPartial()
        {
            var model = galleryRepo.GetAlbums().OrderByDescending(d => d.CreatedDate).Where(a => a.CoverImage != null).PickRandom();
            return PartialView(model);
        }
    }
}