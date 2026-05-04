using Hotel.DTOs.Hotel;

namespace Hotel.Contract
{
    public interface IHotelServices
    {
        
        Task<GetHotelDTO> GetHotelDTO(int id);
        Task<IEnumerable<GetHotelsDto>> GetHotelsDto();
        Task<GetHotelsDto?> CreateHotelDto(CreateHotelDTO newHotel);
        Task<UpdateHotelDTO?> UpdateHotelDto(int Id, UpdateHotelDTO updatedHotel);
        Task DeleteHotel(int id);
        Task<bool> hotelExistAsync(int id);
        Task<bool> hotelExistAsync(string name);
    }
}