using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> Get(int? id = null);
        Task Create(Book book);
        Task Update(Book book);
        Task Delete(int bookId);
    }
}
