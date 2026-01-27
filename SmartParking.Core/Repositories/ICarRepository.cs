using SmartParking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Core.Repositories
{
    public interface ICarRepository
    {
        public Task<IEnumerable<Car>> GetAllAsync();

        public Task<Car> GetByIdAsync(int id);

        public Task<Car> GetByLicenseNumAsync(string licenseNum);
        public Task<Car> AddAsync(Car value);

        public Task<Car> UpdateAsync(int id, Car value);

        public Task DeleteAsync(int id);

        public Task SaveAsync();
    }
}
