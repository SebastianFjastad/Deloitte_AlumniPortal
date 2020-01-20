using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Entities;
using AlumniPortal.Models;

namespace AlumniPortal.Areas.Admin.Models
{
    public class AlumniViewModel : ViewModelBase
    {
        public AlumniViewModel()
        {
            Alumnus = new Alumnus();
            Alumni = new List<Alumnus>();
        }

        public Alumnus Alumnus { get; set; }

        public List<Alumnus> Alumni { get; set; }
    }
}