using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Seed
{
    public static class SeedData
    {
        private static DataContext _context;
        public static void EnsureDbCreatedAndSeeded(this DataContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            context.Database.Migrate();

            SeedRoles(roleManager);
            SeedUsers(userManager);


            context.SaveChanges();
            context.DetachUnchangedEntries();
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new IdentityRole[]
                {
                    new IdentityRole
                    {
                        Name = "Admin"
                    },
                    new IdentityRole
                    {
                        Name = "Benutzer"
                    }
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new IdentityUser[]
                {
                    new IdentityUser
                    {
                        Email = "admin@example.ch",
                        UserName = "Admin12345"
                    },
                    new IdentityUser
                    {
                        Email = "max_muster@example.ch",
                        UserName = "MaxMuster"
                    }
                };

                foreach (var user in users)
                {
                    userManager.CreateAsync(user, "sqlPHP3306$").Wait();
                }

                userManager.AddToRoleAsync(users[0], "Admin").Wait();
                userManager.AddToRoleAsync(users[1], "Benutzer");
            }
        }
    }
}
