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
    public class PublishingHouseService : IPublishingHouseService
    {
        private readonly IPublishingHouseRepository _repository;

        public PublishingHouseService(IPublishingHouseRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<PublishingHouse>> GetAll() => await _repository.Get();
        public async Task<PublishingHouse> GetById(int id) => (await _repository.Get(id)).FirstOrDefault();
        public async Task Create(PublishingHouse publishingHouse) => await _repository.Create(publishingHouse);
        public async Task Update(PublishingHouse publishingHouse) => await _repository.Update(publishingHouse);
        public async Task Delete(int id) => await _repository.Delete(id);
    }
}
