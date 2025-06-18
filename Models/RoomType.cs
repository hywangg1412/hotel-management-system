namespace Models
{
    public partial class RoomType
    {
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public string TypeDescription { get; set; }
        public string TypeNote { get; set; }

        public RoomType()
        {
        }

        public RoomType(int roomTypeId, string roomTypeName, string typeDescription, string typeNote)
        {
            RoomTypeId = roomTypeId;
            RoomTypeName = roomTypeName;
            TypeDescription = typeDescription;
            TypeNote = typeNote;
        }
    }
}
