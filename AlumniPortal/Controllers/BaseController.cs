using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Repositories;

namespace AlumniPortal.Controllers
{
    public class BaseController : Controller
    {
        protected EventRepository eventRepo = new EventRepository();
        protected NewsRepository newsRepo = new NewsRepository();
        protected GalleryRepository galleryRepo = new GalleryRepository();
        protected CareerRepository careerRepo = new CareerRepository();
        protected AlumnusRepository alumniRepo = new AlumnusRepository();
        protected UserRepository userRepo = new UserRepository();
        protected SpecialsRepository specialsRepo = new SpecialsRepository();
        protected MyConnectRepository myConnectRepo = new MyConnectRepository();
        
        public BaseController()
        {
            
        }
    }
}