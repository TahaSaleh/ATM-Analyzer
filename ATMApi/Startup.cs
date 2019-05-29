using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using ATMApi.Models;

[assembly: OwinStartup(typeof(ATMApi.Startup))]

namespace ATMApi
{
    public partial class Startup
    {
        ApplicationDbContext db=new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRules();
            createUsers();

        }
        void createUsers()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = new ApplicationUser();
            user.UserName = "TahaSaleh";
            user.Email = "eng.taha28@yahoo.com";
            IdentityResult result = userManager.Create(user, "Taha248");
            if(result.Succeeded)
            {
                userManager.AddToRole(user.Id, "BAdmin");
            }
        }
        void createRules()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if(!roleManager.RoleExists("BAdmin"))
            {
                role = new IdentityRole();
                role.Name = "BAdmin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("User"))
            {
                role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
