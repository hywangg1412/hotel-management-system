using Bussiness.Interfaces;
using Models;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Context.Common;

namespace Bussiness
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService()
        {
            _roomTypeRepository = new RoomTypeRepository();
        }

        public void Add(RoomType entity)
        {
            try
            {
                AppLogger.LogInformation($"Adding room type: {entity.RoomTypeName}");
                _roomTypeRepository.Add(entity);
                AppLogger.LogInformation($"Room type added successfully: {entity.RoomTypeName}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error adding room type: {entity.RoomTypeName}");
                throw;
            }
        }

        public void Delete(RoomType entity)
        {
            try
            {
                AppLogger.LogInformation($"Deleting room type: {entity.RoomTypeName}");
                _roomTypeRepository.Delete(entity);
                AppLogger.LogInformation($"Room type deleted successfully: {entity.RoomTypeName}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error deleting room type: {entity.RoomTypeName}");
                throw;
            }
        }

        public List<RoomType> GetAll()
        {
            try
            {
                AppLogger.LogInformation("Getting all room types");
                var result = _roomTypeRepository.GetAll();
                AppLogger.LogInformation($"Found {result.Count} room types");
                return result;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, "Error getting all room types");
                throw;
            }
        }

        public RoomType GetById(int id)
        {
            try
            {
                AppLogger.LogInformation($"Getting room type by ID: {id}");
                var roomType = _roomTypeRepository.GetById(id);
                if (roomType == null)
                {
                    AppLogger.LogWarning($"Room type not found with ID: {id}");
                }
                else
                {
                    AppLogger.LogInformation($"Room type found with ID: {id}");
                }
                return roomType;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting room type by ID: {id}");
                throw;
            }
        }

        public List<RoomType> Search(string keyword)
        {
            try
            {
                AppLogger.LogInformation($"Searching room types with keyword: {keyword}");
                var result = _roomTypeRepository.Search(keyword);
                AppLogger.LogInformation($"Found {result.Count} room types matching keyword: {keyword}");
                return result;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error searching room types with keyword: {keyword}");
                throw;
            }
        }

        public void Update(RoomType entity)
        {
            try
            {
                AppLogger.LogInformation($"Updating room type: {entity.RoomTypeName}");
                _roomTypeRepository.Update(entity);
                AppLogger.LogInformation($"Room type updated successfully: {entity.RoomTypeName}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error updating room type: {entity.RoomTypeName}");
                throw;
            }
        }
    }
}
