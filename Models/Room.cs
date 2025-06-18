namespace Models
{
    public partial class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public string RoomStatus { get; set; }
        public decimal RoomPricePerDate { get; set; }
        public int RoomTypeId { get; set; }

        public Room()
        {
        }

        public Room(int roomId, string roomNumber, string roomDescription, int roomMaxCapacity, string roomStatus
            , decimal roomPricePerDate, int roomTypeId)
        {
            RoomId = roomId;
            RoomNumber = roomNumber;
            RoomDescription = roomDescription;
            RoomMaxCapacity = roomMaxCapacity;
            RoomStatus = roomStatus;
            RoomTypeId = roomTypeId;
        }
    }
}
