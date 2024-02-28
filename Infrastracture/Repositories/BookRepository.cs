using Core.IRepositories;
using Core.Models;
using Infrastracture.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repositories
{
    public sealed class BookRepository : IBookRepository
    {
        private readonly SqlDbContext _dbContext;

        public BookRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Book>> Get(int? id = null)
        {
            var query = _dbContext.books
                        .Include(a => a.Author)
                        .Include(p => p.PublishingHouse)
                        .AsQueryable();
            if (id != null)
                query = query.Where(b => b.Id == id);
            return await query.ToListAsync();
        }
        public async Task Create(Book book)
        {
            await _dbContext.books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(Book book)
        {
            var existingBook = await _dbContext.books.FindAsync(book.Id);
            if (existingBook is null)
                throw new InvalidOperationException($"Książka o ID {book.Id} nie została znaleziona.");

            _dbContext.Entry(existingBook).CurrentValues.SetValues(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(int bookId)
        {
            var bookToDelete = await _dbContext.books.FindAsync(bookId);
            if (bookToDelete is null)
                throw new InvalidOperationException($"Książka o ID {bookId} nie została znaleziona.");

            _dbContext.books.Remove(bookToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
