using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            Comments = new List<CommentViewModel>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public CategoryViewModel Category { get; set; }
        public UserViewModel User { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
