using Blog.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // DB-Set Properties for Entities in Project
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Post>().ToTable("Post");
            builder.Entity<Comment>().ToTable("Comment");

            builder.Entity<Category>().Property<int>(x => x.CategoryId)
                .ValueGeneratedOnAdd();
            builder.Entity<Post>().Property<int>(x => x.PostId)
                .ValueGeneratedOnAdd();
            builder.Entity<Comment>().Property<int>(x => x.CommentId)
                .ValueGeneratedOnAdd();
        }
    }
}
