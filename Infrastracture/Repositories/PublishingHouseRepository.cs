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
    public sealed class PublishingHouseRepository : IPublishingHouseRepository
    {
        private readonly SqlDbContext _dbContext;

        public PublishingHouseRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<PublishingHouse>> Get(int? id = null)
        {
            var query = _dbContext.publishingHouses.AsQueryable();
            if (id != null)
                query = query.Where(b => b.Id == id);
            return await query.ToListAsync();
        }
        public async Task Create(PublishingHouse publishingHouse)
        {
            await _dbContext.publishingHouses.AddAsync(publishingHouse);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(PublishingHouse publishingHouse)
        {
            var existingPublishingHouse = await _dbContext.books.FindAsync(publishingHouse.Id);
            if (existingPublishingHouse is null)
                throw new InvalidOperationException($"Wydawca o ID {publishingHouse.Id} nie został znaleziony.");

            _dbContext.Entry(existingPublishingHouse).CurrentValues.SetValues(publishingHouse);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(int bookId)
        {
            var publishingHouseToDelete = await _dbContext.books.FindAsync(bookId);
            if (publishingHouseToDelete is null)
                throw new InvalidOperationException($"Wydawca o ID {bookId} nie został znaleziony.");

            _dbContext.books.Remove(publishingHouseToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
