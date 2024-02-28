using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IPublishingHouseRepository
    {
        Task<List<PublishingHouse>> Get(int? id = null);
        Task Create(PublishingHouse publishingHouse);
        Task Update(PublishingHouse publishingHouse);
        Task Delete(int bookId);
    }
}
