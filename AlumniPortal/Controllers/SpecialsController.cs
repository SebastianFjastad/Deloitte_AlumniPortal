using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Entities;

namespace AlumniPortal.Controllers
{
    public class SpecialsController : BaseController
    {
        public ActionResult Index()
        {
            var model = specialsRepo.GetSpecials().Take(4).ToList();
            return View(model);
        }

        public ActionResult GetSpecialsPage(int pageNo = 1, int pageSize = 4)
        {
            List<Special> model =
                specialsRepo.GetSpecials()
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

            return PartialView(model);
        }

        public ActionResult Special(int id)
        {
            var model = specialsRepo.GetSpecial(id);
            return View(model);
        }

        
    }
}