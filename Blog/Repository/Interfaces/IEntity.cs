using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
