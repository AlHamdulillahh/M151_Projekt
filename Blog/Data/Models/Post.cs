using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
