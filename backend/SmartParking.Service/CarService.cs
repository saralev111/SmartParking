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
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ISpotRepository _spotRepository;
        private readonly IParkingRepository _parkingRepository;
        public CarService(ICarRepository carRepository, ISpotRepository spotRepository, IParkingRepository parkingRepository)
        {
            _carRepository = carRepository;

            _spotRepository = spotRepository;
            _parkingRepository = parkingRepository;
        }
        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _carRepository.GetAllAsync();
        }
        public async Task<Car> GetByIdAsync(int Id)
        {
            return await _carRepository.GetByIdAsync(Id);
        }
        public async Task<Car> GetByLicenseNumAsync(string licenseNum)
        {
            return await _carRepository.GetByLicenseNumAsync(licenseNum);
        }
        public async Task<Car> AddAsync(Car car, int parkingId)
        {
            // 1. בדיקה: האם החניון קיים?
            var parking = await _parkingRepository.GetByIdAsync(parkingId);
            if (parking == null) throw new Exception("Parking lot not found");

            // 2. בדיקה: האם יש מקום פנוי?
            if (parking.Available_spots <= 0) throw new Exception("Parking is full!");

            // 3. מציאת חניה פנויה
            var allSpots = await _spotRepository.GetAllAsync();
            var freeSpot = allSpots.FirstOrDefault(s => s.ParkingId == parkingId && !s.Is_occupied);

            if (freeSpot == null) throw new Exception("No free spot found technically");

            // 4. הוספת הרכב ושמירה כדי לקבל ID
            car.Entry_time = DateTime.Now;
            var newCar = await _carRepository.AddAsync(car);
            await _carRepository.SaveAsync();

            // 5. עדכון החניה הספציפית
            freeSpot.Is_occupied = true;
            freeSpot.CarId = newCar.Id;
            await _spotRepository.UpdateAsync(freeSpot.Id, freeSpot);
            await _spotRepository.SaveAsync();

            // 6. עדכון מונה החניות בחניון
            parking.Available_spots = parking.Available_spots - 1;
            await _parkingRepository.UpdateAsync(parking.Id, parking);
            await _parkingRepository.SaveAsync();

            return newCar;
        }
        public async Task<double> DeleteAsync(int id)
        {
            // 1. שליפת הרכב
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null) throw new Exception("Car not found");

            double paymentToPay = 0;

            // 2. איתור החניה והחניון לחישוב תשלום ושחרור
            var spots = await _spotRepository.GetAllAsync();
            var occupiedSpot = spots.FirstOrDefault(s => s.CarId == id);

            if (occupiedSpot != null)
            {
                // מציאת החניון כדי לדעת מה המחיר לשעה
                var parking = await _parkingRepository.GetByIdAsync(occupiedSpot.ParkingId);

                if (parking != null)
                {
                    //  חישוב התשלום 
                    var timeStayed = DateTime.Now - car.Entry_time;
                    double hoursToCharge = Math.Ceiling(timeStayed.TotalHours); // עיגול תמיד כלפי מעלה
                    paymentToPay = hoursToCharge * parking.Price_per_hour;

                    //  עדכון חניון והגדלת כמות המקומות הפנויים
                    parking.Available_spots += 1;
                    await _parkingRepository.UpdateAsync(parking.Id, parking);
                }

                // שחרור החניה
                occupiedSpot.Is_occupied = false;
                occupiedSpot.CarId = null;
                await _spotRepository.UpdateAsync(occupiedSpot.Id, occupiedSpot);
            }

            // 3. אחרי החישוב, ניתן למחוק את הרכב
            await _carRepository.DeleteAsync(id);

            //שמירת השינויים
            await _spotRepository.SaveAsync();
            await _parkingRepository.SaveAsync();
            await _carRepository.SaveAsync();

            return paymentToPay; // מחזירים את הסכום לתשלום
        }
        public async Task<Car> UpdateAsync(int id, Car value)
        {
            var car = await _carRepository.UpdateAsync(id, value);
            await _carRepository.SaveAsync();
            return car;
        }
    }

}
