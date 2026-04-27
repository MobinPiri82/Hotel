using Hotel.Data;

namespace Hotel.DTOs.Hotel;


public record GetHotelsDto
(
    int id,
    string Name,
    string Address,
    double Rate,
    int CountryId
);
public record GetHotelDTO
(
    int id,
    string Name,
    string Address,
    double Rate,
    string Country
);