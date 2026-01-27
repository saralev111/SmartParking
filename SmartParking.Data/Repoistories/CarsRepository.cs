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
            // שליפת כל הרשימה
            return await _context.cars.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            // תיקון: שימוש ישיר ב-Find (יעיל יותר מ-ToList ואז Find)
            return await _context.cars.FirstAsync(s=>s.Id==id);
        }

        public async Task<Car> GetByLicenseNumAsync(string licenseNum)
        {
            // מחפש בטבלה רכב שהמספר שלו זהה למספר שהתקבל
            return await _context.cars.FirstOrDefaultAsync(c => c.License_num == licenseNum);
        }
        public async Task<Car> AddAsync(Car value) // הוספת async ו-Task
        {
            _context.cars.Add(value);
            return value; // החזרה רגילה, ה-async יעטוף ב-Task
        }

        public async Task DeleteAsync(int id)
        {
            // משתמשים בפונקציה האסינכרונית שכבר קיימת בריפו
            var car = await GetByIdAsync(id);
            if (car != null)
            {
                _context.cars.Remove(car);
            }
            // לא קוראים כאן ל-Save, כי יש פונקציית SaveAsync נפרדת
        }
        // התיקון הקריטי: הוספנו את הפרמטר השני Car value כדי להתאים לממשק
        public async Task<Car> UpdateAsync(int id, Car value)
        {
            // 1. שליפת הרכב הקיים באמצעות הפונקציה שכבר כתבנו
            var existingCar = await GetByIdAsync(id);

            // בדיקה שהרכב אכן נמצא לפני העדכון
            if (existingCar != null)
            {
                // 2. עדכון השדות (מיפוי ידני מהאובייקט שהתקבל לאובייקט הקיים)
                existingCar.License_num = value.License_num;
                existingCar.Owner_name = value.Owner_name;
                existingCar.Entry_time = value.Entry_time;
                existingCar.Exit_time = value.Exit_time;
                existingCar.Total_payment = value.Total_payment;
            }

            // 3. החזרת האובייקט המעודכן (השינויים נשמרים ב-Context בלבד בשלב זה)
            return existingCar;
        }

        public void Delete(int id)
        {
            // שליפה ומחיקה יעילה
            var car = _context.cars.Find(id);
            if (car != null)
            {
                _context.cars.Remove(car);
                _context.SaveChanges(); // חובה לשמור!
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}