namespace SmartParking.Models
{
    public class SpotPutModel
    {
        public int Id { get; set; }
        public bool Is_occupied { get; set; }
        public int CarId { get; set; } 
    }
}
