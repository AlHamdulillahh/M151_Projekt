using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
    }
}
