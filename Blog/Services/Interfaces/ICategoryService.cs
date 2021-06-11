using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> Get(int id);
        Task<Category> Get(int id, string include);
        Task<Category> Add(Category category);
        Task<int> Delete(Category category);
    }
}
