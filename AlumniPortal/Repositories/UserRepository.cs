using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AlumniPortal.Entities;
using AlumniPortal.Models;
using AlumniPortal.Areas.Admin.Models;
using AlumniPortal.DbContext;
using AlumniPortal.Utilities;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin.Security.Providers.ArcGISOnline.Provider;

namespace AlumniPortal.Repositories
{
    public class UserRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUser GetUser(string id)
        {
            return db.Users.Include(u => u.ProfilePic).FirstOrDefault(x => x.Id == id);
        }

        public List<ApplicationUser> GetUsers()
        {
            return db.Users.Where(u => u.AccountActive && u.AccountConfirmed).ToList();
        }

        public List<ApplicationUser> GetRandomUsers(int num, List<string>ids)
        {
            IEnumerable<ApplicationUser> randUsers = new List<ApplicationUser>();
            //filter out the users who are currently on the page so that they are not reloaded
            var rejectList = ids.Select(id => db.Users.Find(id)).ToList();

            var allUsers = db.Users.ToList();

            var duplicates = from u in rejectList
                             from a in allUsers
                             where (u.Id == a.Id)
                             select a;

            //get only if user is User role
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.Roles.FirstOrDefault(r => r.Name == "User");
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var usersWithoutExisting = allUsers.Where(u => userManager.IsInRole(u.Id, role.Name)).Except(duplicates);

            randUsers = usersWithoutExisting.PickRandom(6);
            return randUsers.ToList();
        }

        public bool CreateUser(ApplicationUser user, RoleType role = RoleType.User)
        {
            if (string.IsNullOrEmpty(user.Id))
            {
                //add NEW user
                var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));
                manager.Create(user);

                //need to check if enum to string conversion works
                manager.AddToRole(user.Id, role.ToString());
            }
            return false;
        }

        public bool SaveUser(ApplicationUser user, RoleType role = RoleType.User)
        {
            var manager = new UserManager<ApplicationUser>(
                   new UserStore<ApplicationUser>(
                       new ApplicationDbContext()));

            if (!string.IsNullOrEmpty(user.Id))
            {
                manager.Create(user);
                manager.AddToRole(user.Id, role.ToString());
            }
            else
            {
                manager.Update(user);
            }
            return false;
        }

        public List<ApplicationUser> SearchUsers(string term, string currUserId)
        {
            //to solve recursive json serialization error
            db.Configuration.ProxyCreationEnabled = false;
            var users = db.Users.Where(a => a.FirstName.Contains(term) || a.LastName.Contains(term)).ToList();
            var currUserToRemove = users.SingleOrDefault(u => u.Id == currUserId);

            //remove the searching user from the list
            users.Remove(currUserToRemove);

            return users;
        }

        //Soft Delete because we only Deactivate an account, never delete 
        public bool SoftDeleteUser(string id)
        {
            var user = db.Users.Find(id);
            user.AccountActive = false;
            db.SaveChanges();
            return false;
        }

        //if a user is soft deleted (inactive), reactivate
        public bool ActivateUser(string id)
        {
            var user = db.Users.Find(id);
            user.AccountActive = false;
            db.SaveChanges();
            return false;
        }

        //confirm user who could not be authenticated automatically
        public bool AcceptNewUserApplication(string id)
        {
            var user = db.Users.Find(id);
            user.AccountConfirmed = true;
            db.SaveChanges();
            return false;
        }
    }
}