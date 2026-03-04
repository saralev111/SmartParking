using SmartParking.Core.Entities;
using SmartParking.Core.Repositories;
using SmartParking.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Service
{
    public class SpotService: ISpotService
    {
        private readonly ISpotRepository _spotRepository;

        public SpotService(ISpotRepository spotRepository)
        {
            _spotRepository = spotRepository;
        }

        public async Task<IEnumerable<Spot>> GetAllAsync()
        {
            return await _spotRepository.GetAllAsync();
        }

        public async Task<Spot> GetByIdAsync(int id)
        {
            return await _spotRepository.GetByIdAsync(id);
        }

        public async Task<Spot> AddAsync(Spot spot)
        {
            var s = await _spotRepository.AddAsync(spot);
            await _spotRepository.SaveAsync();
            return s;
        }

        public async Task<Spot> UpdateAsync(int id, Spot value)
        {
            var spot = await _spotRepository.UpdateAsync(id, value);
            await _spotRepository.SaveAsync();
            return spot;
        }

        public async Task DeleteAsync(int id)
        {
            await _spotRepository.DeleteAsync(id);
            await _spotRepository.SaveAsync();
        }
    }
}
