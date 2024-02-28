using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> Get(int? id = null);
        Task Create(Author author);
        Task Update(Author author);
        Task Delete(int authorId);
    }
}
