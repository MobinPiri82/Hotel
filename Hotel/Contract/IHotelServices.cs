using Hotel.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Contract
{
    public interface IHotelServices
    {
        Task<GetHotelDTO> GetHotelDTO(int id);
        Task<IEnumerable<GetHotelsDto>> GetHotelsDto();
        Task<GetHotelsDto?> Post(CreateHotelDTO newHotel);
        Task<UpdateHotelDTO> Put(int Id,UpdateHotelDTO updatedHotel);
    }
}