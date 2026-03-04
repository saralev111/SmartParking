using SmartParking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Core.Repositories
{
    public interface IParkingRepository
    {
        Task<IEnumerable<Parking>> GetAllAsync();
        Task<Parking> GetByIdAsync(int id);
        Task<Parking> AddAsync(Parking value);
        Task<Parking> UpdateAsync(int id, Parking value);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
