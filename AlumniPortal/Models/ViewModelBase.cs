using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public class ViewModelBase
    {
        public bool HasErrors { get; set; }
        public string ErrorMessage { get; set; }
    }
}