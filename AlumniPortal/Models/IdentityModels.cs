using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AlumniPortal.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AlumniPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "First name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }
        
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [Display(Name = "Nick name")]
        public string NickName { get; set; }

        [Display(Name = "Job title")]
        public string JobTitle { get; set; }

        [Display(Name = "About you")]
        [AllowHtml]
        public string About { get; set; }

        [Display(Name = "Allow publish email")]
        public bool AllowPubEmail { get; set; }
        
        [Display(Name = "Allow publish number")]
        public bool AllowPubPhoneNo { get; set; }
        
        [Display(Name = "Account active")]
        public bool AccountActive { get; set; }
        
        [Display(Name = "Account confirmed")]
        public bool AccountConfirmed { get; set; }

        public string Region { get; set; }

        public Asset ProfilePic { get; set; }

        public virtual ICollection<EventInvite> EventInvites { get; set; }
    }
}