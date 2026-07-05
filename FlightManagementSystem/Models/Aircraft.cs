namespace FlightManagementSystem.Models
{
    public class Aircraft
    {
        public int aircraftId { get; set; }
        public string model { get; set; }
        public int totalSeats { get; set; }
        public bool isOperational { get; set; }
    }
}