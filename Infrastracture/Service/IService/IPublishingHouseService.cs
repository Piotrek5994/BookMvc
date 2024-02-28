using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Service.IService
{
    public interface IPublishingHouseService
    {
        Task<List<PublishingHouse>> GetAll();
        Task<PublishingHouse> GetById(int id);
        Task Create(PublishingHouse publishingHouse);
        Task Update(PublishingHouse publishingHouse);
        Task Delete(int id);
    }
}
