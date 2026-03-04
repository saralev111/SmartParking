namespace SmartParking.Core.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string License_num { get; set; }
        public string Owner_name { get; set; }
        public DateTime Entry_time { get; set; }
        public DateTime Exit_time { get; set; }
        public double Total_payment { get; set; }
        public Spot Spot { get; set; }
    }
}
