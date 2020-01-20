using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AlumniPortal.DbContext;
using AlumniPortal.Entities;
using AlumniPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AlumniPortal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole {Name = "Admin"};

                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "ContentAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole {Name = "ContentAdmin"};

                roleManager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole {Name = "User"};

                roleManager.Create(role);
            }

            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));

            //Add Users
            //for (int i = 5; i < 10; i++)
            //{
            //    var user = new ApplicationUser()
            //    {
            //        UserName = string.Format("user{0}", i),
            //        FirstName = string.Format("Name{0}", i),
            //        Email = string.Format("user{0}@user.com", i),
            //        PhoneNumber = string.Format((111111111 * i).ToString()),
            //        Region = "Gauteng",
            //        AccountConfirmed = true,
            //        AccountActive = true
            //    };
            //    manager.Create(user, "password123");
            //    manager.AddToRole(user.Id, "User");
            //}

            //var userNew = new ApplicationUser()
            //{
            //    UserName = "user@user.com",
            //    FirstName = "user",
            //    Email = "user@user.com",
            //    PhoneNumber = "11223344",
            //    Region = "Western Cape",
            //    AccountConfirmed = true,
            //    AccountActive = true
            //};

            //manager.Create(userNew, "password123");
            //manager.AddToRole(userNew.Id, "User");

            var userNew = new ApplicationUser()
            {
                UserName = "user1@user.com",
                FirstName = "user",
                Email = "user1@user.com",
                PhoneNumber = "11223344",
                Region = "Western Cape",
                AccountConfirmed = true,
                AccountActive = true
            };

            manager.Create(userNew, "password123");
            manager.AddToRole(userNew.Id, "User");



            //Add Admin
            //var admin = new ApplicationUser()
            //{
            //    UserName = "admin@admin.com",
            //    FirstName = "Admin",
            //    Email = "admin@admin.com",
            //    PhoneNumber = "11223344",
            //    AccountConfirmed = true,
            //    AccountActive = true
            //};
            //manager.Create(admin, "admin123");
            //manager.AddToRole(admin.Id, "Admin");



            ////Add ContentAdmin
            //var contentAdmin = new ApplicationUser()
            //{
            //    UserName = "contentadmin@contentadmin.com",
            //    FirstName = "Contentadmin",
            //    Email = "contentadmin@contentadmin.com",
            //    PhoneNumber = "11223344",
            //    AccountConfirmed = true,
            //    AccountActive = true
            //};
            //manager.Create(contentAdmin, "contentadmin");
            //manager.AddToRole(contentAdmin.Id, "ContentAdmin");

        }

        private void SaveChanges(ApplicationDbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); 
            }
        }
    }
}

