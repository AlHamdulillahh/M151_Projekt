using Blog.Data.Models;
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
            SeedCategories();
            SeedPosts();
            SeedComments();


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
                userManager.AddToRoleAsync(users[1], "Benutzer").Wait();
            }
        }

        private static void SeedCategories()
        {
            if (!_context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category
                    {
                        Id = 1,
                        Name = "Web-Development"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Mobile-Development"
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Python"
                    }
                };

                _context.Categories.AddRange(categories);
            }
        }

        private static void SeedPosts()
        {
            if (!_context.Posts.Any())
            {
                var posts = new Post[]
                {
                    new Post
                    {
                        Id = 1,
                        Title = "What's new in PHP 8",
                        Body = "Union Types: Given the dynamically typed nature of PHP, there are lots of cases where union types can be useful.",
                        CategoryId = 1
                    },
                    new Post
                    {
                        Id = 2,
                        Title = "5 Important Python One-Liners",
                        Body = "The One-Liners showed in this post will make your life so much easier",
                        CategoryId = 3
                    }
                };

                _context.Posts.AddRange(posts);
            }
        }

        private static void SeedComments()
        {
            if (!_context.Comments.Any())
            {
                var comments = new Comment[]
                {
                    new Comment
                    {
                        Id = 1,
                        Body = "Great post!",
                        PostId = 1
                    },
                    new Comment
                    {
                        Id = 2,
                        Body = "Good post. Helped alot",
                        PostId = 2
                    }
                };

                _context.Comments.AddRange(comments);
            }
        }
    }
}
