using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Core.DTOs
{
    public class SpotDTO
    {
        public int Id { get; set; }
        public int Spot_number { get; set; }
        public bool Is_occupied { get; set; }
        //public int ParkingId { get; set; }
        // כמו שהמורה עשתה עם ClassDTO בתוך StudentDTO:
        public ParkingDTO Parking { get; set; }
    }
}
