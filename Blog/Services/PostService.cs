using Blog.Data.Models;
using Blog.Repository.Interfaces;
using Blog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Post> Add(Post post)
        {
            _unitOfWork.Posts.Add(post);
            await _unitOfWork.CompleteAsync();
            return post;
        }

        public async Task<int> Delete(Post post)
        {
            _unitOfWork.Posts.Remove(post);
            return await _unitOfWork.CompleteAsync();
        }

        public Task<Post> Get(int id, string include)
        {
            return _unitOfWork.Posts.GetAsync(id, include);
        }

        public Task<Post> Get(int id, IEnumerable<string> includes)
        {
            return _unitOfWork.Posts.GetAsync(id, includes);
        }

        public Task<List<Post>> GetAll(IEnumerable<string> includes)
        {
            return _unitOfWork.Posts.GetAllAsync(includes);
        }

        public async Task<int> Update(Post post)
        {
            _unitOfWork.Posts.Update(post);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Post>> Search(string queryString)
        {
            return await _unitOfWork.Posts.FindAsync(x => x.Title.Contains(queryString)
            || x.Body.Contains(queryString));
        }
    }
}
