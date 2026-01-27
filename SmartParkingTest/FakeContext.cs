using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartParking;
using SmartParking.Entities;


namespace SmartParkingTest
{
    public class FakeContext: IDataContext
    {
        public List<Car> cars { get; set; }
        public List<Parking> parkings { get; set; }

        public List<Spot> spots { get; set; }


        public FakeContext()
        {
            cars = new List<Car> { new Car { Id = 2, License_num = "23456789", Owner_name = "sara", Entry_time = new DateTime(), Exit_time = new DateTime(), Total_payment = 400 } };
            parkings = new List<Parking> { new Parking { Id = 2, Name = "23456789", Location = "bnei brak", Total_spots = 200, Available_spots = 80, Price_per_hour = 50 } };
            spots = new List<Spot> { new Spot { Id = 2, Parking_id = 3, Spot_number = 170, Is_occupied = false, Vehicle_id = false } };

        }
    }
}
