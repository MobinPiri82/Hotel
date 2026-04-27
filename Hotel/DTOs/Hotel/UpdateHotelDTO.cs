using System.ComponentModel.DataAnnotations;

namespace Hotel.DTOs.Hotel
{
    public class UpdateHotelDTO : CreateHotelDTO
    {
        [Required]
        public int id{ get; set; }
    }
    
}
