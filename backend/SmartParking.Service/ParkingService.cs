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
    public class ParkingService : IParkingService
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly ISpotRepository _spotRepository;

        public ParkingService(IParkingRepository parkingRepository, ISpotRepository spotRepository)
        {
            _parkingRepository = parkingRepository;
            _spotRepository = spotRepository;
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
            // 1. יצירת חניון
            var p = await _parkingRepository.AddAsync(parking);
            await _parkingRepository.SaveAsync();

            // 2. יצירת החניות בתוך החניון באופן אוטומטי
            for (int i = 1; i <= parking.Total_spots; i++)
            {
                var newSpot = new Spot
                {
                    ParkingId = p.Id,      // מקשרים לחניון החדש
                    Spot_number = i,       // חניה מספר 1, 2, 3...
                    Is_occupied = false    // בהתחלה הן פנויות
                };
                await _spotRepository.AddAsync(newSpot);
            }

            // 3. שומרים את כל החניות שיצרנו
            await _spotRepository.SaveAsync();

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
