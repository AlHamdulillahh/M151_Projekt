using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<int> Add(Comment comment);
    }
}
