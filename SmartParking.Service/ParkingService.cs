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
    public class ParkingService: IParkingService
    {
        private readonly IParkingRepository _parkingRepository;

        public ParkingService(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        public async Task<IEnumerable<Parking>> GetAllAsync()
        {
            return await _parkingRepository.GetAllAsync();
        }

        public async Task<Parking> GetByIdAsync(int id)
        {
            return await _parkingRepository.GetByIdAsync(id);
        }

        public async Task<Parking> AddAsync(Parking parking)
        {
            var p = await _parkingRepository.AddAsync(parking);
            await _parkingRepository.SaveAsync();
            return p;
        }

        public async Task<Parking> UpdateAsync(int id, Parking value)
        {
            var parking = await _parkingRepository.UpdateAsync(id, value);
            await _parkingRepository.SaveAsync();
            return parking;
        }

        public async Task DeleteAsync(int id)
        {
            await _parkingRepository.DeleteAsync(id);
            await _parkingRepository.SaveAsync();
        }
    }
}
