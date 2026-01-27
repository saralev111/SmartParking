namespace SmartParking.Core.Entities
{
    public class Spot
    {
        public int Id { get; set; }
        public int Spot_number { get; set; }
        public bool Is_occupied { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public Parking Parking { get; set; }
    }
}
