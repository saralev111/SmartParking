using SmartParking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Core.Services
{
    public interface ISpotService
    {
        Task<IEnumerable<Spot>> GetAllAsync();
        Task<Spot> GetByIdAsync(int id);
        Task<Spot> AddAsync(Spot value);
        Task<Spot> UpdateAsync(int id, Spot value);
        Task DeleteAsync(int id);
    }
}
