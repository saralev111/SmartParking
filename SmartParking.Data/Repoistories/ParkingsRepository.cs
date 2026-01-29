using Microsoft.EntityFrameworkCore;
using SmartParking.Core.Entities;
using SmartParking.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Data.Repoistories
{
    public class ParkingsRepository: IParkingRepository
    {
        private readonly DataContext _context;

        public ParkingsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Parking>> GetAllAsync()
        {
            return await _context.parkings.ToListAsync();
        }

        public async Task<Parking> GetByIdAsync(int id)
        {
            return await _context.parkings.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Parking> AddAsync(Parking value)
        {
            _context.parkings.Add(value);
            return value;
        }
        public async Task<Parking> UpdateAsync(int id, Parking value)
        {
            var existingParking = await GetByIdAsync(id);
            if (existingParking != null)
            {
                existingParking.Name = value.Name;
                existingParking.Location = value.Location;
                existingParking.Total_spots = value.Total_spots;
                existingParking.Available_spots = value.Available_spots;
                existingParking.Price_per_hour = value.Price_per_hour;
            }
            return existingParking;
        }

        public async Task DeleteAsync(int id)
        {
            var parking = await GetByIdAsync(id);
            if (parking != null)
            {
                _context.parkings.Remove(parking);
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
