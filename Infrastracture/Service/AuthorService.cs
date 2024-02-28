using Core.IRepositories;
using Core.Models;
using Infrastracture.Service.IService;

namespace Infrastracture.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Author>> GetAll() => await _repository.Get();
        public async Task<Author> GetById(int id) => (await _repository.Get(id)).FirstOrDefault();
        public async Task Create(Author author) => await _repository.Create(author);
        public async Task Update(Author author) => await _repository.Update(author);
        public async Task Delete(int id) => await _repository.Delete(id);
    }
}
