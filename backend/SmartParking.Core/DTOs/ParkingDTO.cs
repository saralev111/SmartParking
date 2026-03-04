using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Core.DTOs
{
    public class ParkingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Available_spots { get; set; }
        public double Price_per_hour { get; set; }
    }
}
