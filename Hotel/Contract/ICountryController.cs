using Hotel.DTOs.Country;

namespace Hotel.Contract;


public interface ICountryInterface
{
    Task<GetCountryDTO> CreateCountry(CreateCountryDTO newCountry);
    Task DeleteCountry(int id);
    Task<IEnumerable<GetCountriesDTO>> GetCountries();
    Task<GetCountryDTO> GetCountry(int id);
    Task<CreateCountryDTO> UpdateCountry(int id, UpdateCountryDTO UpdatingCountry);
}


