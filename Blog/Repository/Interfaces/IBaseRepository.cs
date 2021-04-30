using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.Interfaces
{
    public interface IBaseRepository<TEntity, in TKey> where TEntity : class
    {

    }
}
