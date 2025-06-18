using DataAccess.Context.Common;
using DataAccess.Interfaces;
using Models;

namespace DataAccess
{
    public class RoomRepository : IRoomRepository
    {
        private static List<Room> _roomList;

        public RoomRepository()
        {
            _roomList = new List<Room>();
        }
        public void Add(Room entity)
        {
            try
            {
                _roomList.Add(entity);
                AppLogger.LogInformation($"Room added: {entity.RoomNumber}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error adding room: {entity.RoomNumber}");
                throw;
            }
        }

        public void Delete(Room entity)
        {
            try
            {
                _roomList.Remove(entity);
                AppLogger.LogInformation($"Room deleted: {entity.RoomNumber}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error deleting room: {entity.RoomNumber}");
                throw;
            }
        }

        public List<Room> GetAll()
        {
            try
            {
                AppLogger.LogInformation("Getting all rooms");
                return _roomList;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, "Error getting all rooms");
                throw;
            }
        }

        public Room GetById(int id)
        {
            try
            {
                AppLogger.LogInformation($"Getting room by ID: {id}");
                var room = _roomList.FirstOrDefault(r => r.RoomID == id);
                if (room == null)
                {
                    AppLogger.LogWarning($"Room not found with ID: {id}");
                }
                return room;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting room by ID: {id}");
                throw;
            }
        }

        public List<Room> Search(string keyword)
        {
            try
            {
                AppLogger.LogInformation($"Searching rooms with keyword: {keyword}");
                var result = _roomList
                    .Where(r => r.RoomNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                               r.RoomDescription.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                AppLogger.LogInformation($"Found {result.Count} rooms matching keyword: {keyword}");
                return result;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error searching rooms with keyword: {keyword}");
                throw;
            }
        }

        public void Update(Room entity)
        {
            try
            {
                var existingRoom = GetById(entity.RoomID);
                if (existingRoom == null)
                {
                    AppLogger.LogWarning($"Room not found for update. ID: {entity.RoomID}");
                    throw new KeyNotFoundException($"Room with ID {entity.RoomID} not found");
                }

                existingRoom.RoomNumber = entity.RoomNumber;
                existingRoom.RoomDescription = entity.RoomDescription;
                existingRoom.RoomMaxCapacity = entity.RoomMaxCapacity;
                existingRoom.RoomStatus = entity.RoomStatus;
                existingRoom.RoomPricePerDate = entity.RoomPricePerDate;
                existingRoom.RoomTypeID = entity.RoomTypeID;

                AppLogger.LogInformation($"Room updated: {entity.RoomNumber}");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error updating room: {entity.RoomNumber}");
                throw;
            }
        }
    }
}
