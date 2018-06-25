namespace webbooks.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using webbooks.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<webbooks.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(webbooks.Models.ApplicationDbContext context)
        {
            #region Usuário Admin
            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            PasswordHasher phaSenha = new PasswordHasher();
            ApplicationUser applicationuser;
            if (!context.Users.Any(u => u.UserName == "admin@webbooks.com.br"))
            {
                applicationuser = new ApplicationUser
                {
                    UserName = "admin@webbooks.com.br",
                    Email = "admin@webbooks.com.br",
                    PasswordHash = phaSenha.HashPassword("admin"),
                    LockoutEnabled = true,
                    EmailConfirmed = true
                };

                IdentityResult ireResultado = usermanager.Create(applicationuser, "admin");
                if (!ireResultado.Succeeded)
                    return;
            }
            else
            {
                applicationuser = usermanager.FindByName("admin@webbooks.com.br");
                usermanager.Update(applicationuser);
            }
            #endregion
        }
    }
}
