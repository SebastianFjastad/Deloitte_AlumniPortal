using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Entities;

namespace AlumniPortal.Models
{
    public class MyConnectViewModel
    {
        public MyConnectViewModel()
        {
            User = new ApplicationUser();
            Conversations = new List<Conversation>();
        }

        public ApplicationUser User { get; set; }
        public List<Conversation> Conversations { get; set; } 
    }
}