using Bussiness.Interfaces;
using Models;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Context.Common;

namespace Bussiness
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService()
        {
            _roomRepository = new RoomRepository();
        }

        public void Add(Room entity)
        {
            try
            {
                AppLogger.LogInformation($"Adding room: {entity.RoomNumber}");
                _roomRepository.Add(entity);
                AppLogger.LogInformation($"Room added successfully: {entity.RoomNumber}");
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
                AppLogger.LogInformation($"Deleting room: {entity.RoomNumber}");
                _roomRepository.Delete(entity);
                AppLogger.LogInformation($"Room deleted successfully: {entity.RoomNumber}");
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
                var result = _roomRepository.GetAll();
                AppLogger.LogInformation($"Found {result.Count} rooms");
                return result;
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
                var room = _roomRepository.GetById(id);
                if (room == null)
                {
                    AppLogger.LogWarning($"Room not found with ID: {id}");
                }
                else
                {
                    AppLogger.LogInformation($"Room found with ID: {id}");
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
                var result = _roomRepository.Search(keyword);
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
                AppLogger.LogInformation($"Updating room: {entity.RoomNumber}");
                _roomRepository.Update(entity);
                AppLogger.LogInformation($"Room updated successfully: {entity.RoomNumber}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error updating room: {entity.RoomNumber}");
                throw;
            }
        }
    }
}
