using DataAccess.Context.Common;
using DataAccess.Interfaces;
using Models;

namespace DataAccess
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private static List<RoomType> _roomTypeList;

        public RoomTypeRepository()
        {
            if (_roomTypeList == null)
            {
                _roomTypeList = new List<RoomType>();
                
                // Add sample data
                var sampleRoomTypes = new List<RoomType>
                {
                    new RoomType
                    {
                        RoomTypeId = 1,
                        RoomTypeName = "Standard",
                        TypeDescription = "Basic room with essential amenities",
                        TypeNote = "Suitable for budget travelers"
                    },
                    new RoomType
                    {
                        RoomTypeId = 2,
                        RoomTypeName = "Deluxe",
                        TypeDescription = "Premium room with enhanced amenities",
                        TypeNote = "Popular choice for business travelers"
                    },
                    new RoomType
                    {
                        RoomTypeId = 3,
                        RoomTypeName = "Suite",
                        TypeDescription = "Luxury suite with premium amenities",
                        TypeNote = "Perfect for special occasions"
                    }
                };
                
                _roomTypeList.AddRange(sampleRoomTypes);
            }
        }
        public void Add(RoomType entity)
        {
            try
            {
                _roomTypeList.Add(entity);
                AppLogger.LogInformation($"RoomType added: {entity.RoomTypeName}");
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
                _roomTypeList.Remove(entity);
                AppLogger.LogInformation($"RoomType deleted: {entity.RoomTypeName}");
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
                return _roomTypeList;
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
                var roomType = _roomTypeList.FirstOrDefault(rt => rt.RoomTypeId == id);
                if (roomType == null)
                {
                    AppLogger.LogWarning($"RoomType not found with ID: {id}");
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
                var result = _roomTypeList
                    .Where(rt => rt.RoomTypeName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                rt.TypeDescription.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

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
                var existingRoomType = GetById(entity.RoomTypeId);
                if (existingRoomType == null)
                {
                    AppLogger.LogWarning($"RoomType not found for update. ID: {entity.RoomTypeId}");
                    throw new KeyNotFoundException($"RoomType with ID {entity.RoomTypeId} not found");
                }

                existingRoomType.RoomTypeName = entity.RoomTypeName;
                existingRoomType.TypeDescription = entity.TypeDescription;
                existingRoomType.TypeNote = entity.TypeNote;

                AppLogger.LogInformation($"RoomType updated: {entity.RoomTypeName}");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error updating room type: {entity.RoomTypeName}");
                throw;
            }
        }
    }
}
