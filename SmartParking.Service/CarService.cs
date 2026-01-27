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
    public class CarService: ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            //לוגיקה חישובים
            return await _carRepository.GetAllAsync();
            // |חישובים
        }
        public async Task<Car> GetByIdAsync(int Id)
        {
            return await _carRepository.GetByIdAsync(Id);
        }
        public async Task<Car> GetByLicenseNumAsync(string licenseNum)
        {
            // הסרביס פשוט מעביר את הבקשה לריפוזיטורי
            return await _carRepository.GetByLicenseNumAsync(licenseNum);
        }
        public async Task<Car> AddAsync(Car car)
        {
            // חובה להוסיף await כדי לקבל את האובייקט ולא את ה-Task
            var s = await _carRepository.AddAsync(car);
            await _carRepository.SaveAsync();
            return s;
        }
        public async Task DeleteAsync(int id)
        {
            // מחיקה מה-context (בזיכרון)
            await _carRepository.DeleteAsync(id);

            // שמירה פיזית במסד הנתונים
            await _carRepository.SaveAsync();
        }
        public async Task<Car> UpdateAsync(int id, Car value)
        {
            var car=await _carRepository.UpdateAsync(id, value);
            await _carRepository.SaveAsync();
            return car;
        }
    }

}
