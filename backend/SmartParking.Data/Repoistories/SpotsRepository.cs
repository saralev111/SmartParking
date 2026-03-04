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
    public class SpotsRepository: ISpotRepository
    {
        private readonly DataContext _context;

        public SpotsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Spot>> GetAllAsync()
        {
            return await _context.spots.ToListAsync();
        }

        public async Task<Spot> GetByIdAsync(int id)
        {
            return await _context.spots.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Spot> AddAsync(Spot value)
        {
            _context.spots.Add(value);
            return value;
        }

        public async Task<Spot> UpdateAsync(int id, Spot value)
        {
            var existingSpot = await GetByIdAsync(id);
            if (existingSpot != null)
            {
                existingSpot.Is_occupied = value.Is_occupied;
                existingSpot.CarId = value.CarId;
            }
            return existingSpot;
        }

        public async Task DeleteAsync(int id)
        {
            var spot = await GetByIdAsync(id);
            if (spot != null)
            {
                _context.spots.Remove(spot);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
