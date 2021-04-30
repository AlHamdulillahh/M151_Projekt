using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
