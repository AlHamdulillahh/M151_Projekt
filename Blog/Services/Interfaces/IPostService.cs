using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAll(IEnumerable<string> includes);
        Task<Post> Get(int id, string include);
        Task<Post> Get(int id, IEnumerable<string> includes);
        Task<Post> Add(Post post);
        Task<int> Update(Post post);
        Task<int> Delete(Post post);
        public Task<List<Post>> Search(string queryString);
        // Search
    }
}
