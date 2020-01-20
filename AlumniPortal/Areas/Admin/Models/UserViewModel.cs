using System.Collections.Generic;
using AlumniPortal.Models;

namespace AlumniPortal.Areas.Admin.Models
{
    public class UserViewModel : ViewModelBase
    {
        public UserViewModel()
        {
            User = new ApplicationUser();
            Users = new List<ApplicationUser>();
        }

        public ApplicationUser User { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}