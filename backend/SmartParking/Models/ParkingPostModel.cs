namespace SmartParking.Models
{
    public class ParkingPostModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Total_spots { get; set; }
        public double Price_per_hour { get; set; }
    }
}
