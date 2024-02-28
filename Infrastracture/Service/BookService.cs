using Core.IRepositories;
using Core.Models;
using Infrastracture.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Book>> GetAll() => await _repository.Get();
        public async Task<Book> GetById(int id) => (await _repository.Get(id)).FirstOrDefault();
        public async Task Create(Book book) => await _repository.Create(book);
        public async Task Update(Book book) => await _repository.Update(book);
        public async Task Delete(int id) => await _repository.Delete(id);
    }
}
