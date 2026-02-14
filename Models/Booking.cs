namespace RoomBooking.Models // <--- Pastikan ini RoomBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Pending";
    }
}