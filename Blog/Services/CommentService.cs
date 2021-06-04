using Blog.Data.Models;
using Blog.Repository.Interfaces;
using Blog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Add(Comment comment)
        {
            _unitOfWork.Comments.Add(comment);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
