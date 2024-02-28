using Core.IRepositories;
using Core.Models;
using Infrastracture.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories
{
    public sealed class AuthorRepository : IAuthorRepository
    {
        private readonly SqlDbContext _dbContext;

        public AuthorRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Author>> Get(int? id = null)
        {
            var query = _dbContext.authors
                        .AsQueryable();
            if (id != null)
                query = query.Where(b => b.Id == id);
            return await query.ToListAsync();
        }
        public async Task Create(Author author)
        {
            await _dbContext.authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(Author author)
        {
            var existingAuthor = await _dbContext.books.FindAsync(author.Id);
            if (existingAuthor is null)
                throw new InvalidOperationException($"Author o ID {author.Id} nie został znaleziony.");

            _dbContext.Entry(existingAuthor).CurrentValues.SetValues(author);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(int authorId)
        {
            var authorToDelete = await _dbContext.books.FindAsync(authorId);
            if (authorToDelete is null)
                throw new InvalidOperationException($"Author o ID {authorId} nie został znaleziony.");

            _dbContext.books.Remove(authorToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
