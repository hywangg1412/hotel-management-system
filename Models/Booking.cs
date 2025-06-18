namespace Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int RoomID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public Booking() { }

        public Booking(int bookingId, int customerId, int roomId, DateTime startdate,
            DateTime endDate, decimal totalPrice, string status)
        {
            BookingID = bookingId;
            CustomerID = customerId;
            RoomID = roomId;
            StartDate = startdate;
            EndDate = endDate;
            TotalPrice = totalPrice;
            Status = status;
        }
    }
}
