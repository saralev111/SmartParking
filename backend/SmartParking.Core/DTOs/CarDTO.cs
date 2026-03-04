using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Core.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string License_num { get; set; }
        public string Owner_name { get; set; }
        public DateTime Entry_time { get; set; }
        public DateTime Exit_time { get; set; }
        public double Total_payment { get; set; }
        // כאן נחזיר רק את מספר החניה או אובייקט DTO של חניה אם צריך
        //public int SpotId { get; set; }
    }
}
