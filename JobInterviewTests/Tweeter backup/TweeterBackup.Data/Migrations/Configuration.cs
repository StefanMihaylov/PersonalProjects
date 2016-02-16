namespace TweeterBackup.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TweeterBackup.Common;
    using TweeterBackup.Data.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AppDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
        }

        private void SeedUsers(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));
            
            const string AdminEmail = "admin@tweeterbackup.com";
            const string AdminPassword = "123456";

            var admin = new User()
            {
                UserName = AdminEmail,  
                Email = AdminEmail,
                Screen_name = "mihaylov_s"
            };

            userManager.Create(admin, AdminPassword);
            userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);
        }

        private void SeedRoles(AppDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
        }
    }
}
