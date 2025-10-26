namespace SmartParking
{
    public class Parking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Total_spots { get; set; }
        public int Available_spots { get; set; }
        public double Price_per_hour { get; set; }
    }
}
