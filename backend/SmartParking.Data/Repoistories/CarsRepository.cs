using Microsoft.EntityFrameworkCore;
using SmartParking.Core.Entities;
using SmartParking.Core.Repositories;
using SmartParking.Data;
using System.Collections.Generic;
using System.Linq;

namespace SmartParking.Data.Repoistories
{
    public class CarsRepository : ICarRepository
    {
        private readonly DataContext _context;

        public CarsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.cars.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.cars.FirstAsync(s=>s.Id==id);
        }

        public async Task<Car> GetByLicenseNumAsync(string licenseNum)
        {
            return await _context.cars.FirstOrDefaultAsync(c => c.License_num == licenseNum);
        }
        public async Task<Car> AddAsync(Car value) 
        {
            _context.cars.Add(value);
            return value; 
        }

        public async Task DeleteAsync(int id)
        {
            var car = await GetByIdAsync(id);
            if (car != null)
            {
                _context.cars.Remove(car);
            }
        }
        public async Task<Car> UpdateAsync(int id, Car value)
        {
            var existingCar = await GetByIdAsync(id);

            if (existingCar != null)
            {
                existingCar.License_num = value.License_num;
                existingCar.Owner_name = value.Owner_name;
                existingCar.Entry_time = value.Entry_time;
                existingCar.Exit_time = value.Exit_time;
                existingCar.Total_payment = value.Total_payment;
            }

            return existingCar;
        }

        public void Delete(int id)
        {
            var car = _context.cars.Find(id);
            if (car != null)
            {
                _context.cars.Remove(car);
                _context.SaveChanges(); 
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}