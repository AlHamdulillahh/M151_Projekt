using Blog.Data;
using Blog.Data.Models;
using Blog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.Repositories
{
    public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }
        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}
