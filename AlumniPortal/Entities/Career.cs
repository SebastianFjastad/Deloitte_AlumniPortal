using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Entities
{
    public class Career
    {
        public int CareerId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Position { get; set; }
        public string TaleoLink { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}