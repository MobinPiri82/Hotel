using Hotel.DTOs.Hotel;

namespace Hotel.DTOs.Country;

public record GetCountriesDTO(
    int CountryId,
    string Name,
    string ShortName
    );

public record GetCountryDTO(
    int CountryId,
    string Name,
    string ShortName,
    List<string> HotelNames
    );
