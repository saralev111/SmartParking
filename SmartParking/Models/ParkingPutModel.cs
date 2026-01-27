namespace SmartParking.Models
{
    public class ParkingPutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price_per_hour { get; set; }
        public int Total_spots { get; set; } // למקרה שהחניון התרחב
    }
}
