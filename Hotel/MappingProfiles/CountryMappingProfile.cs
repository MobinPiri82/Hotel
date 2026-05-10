using AutoMapper;
using Hotel.DTOs.Country;
using Hotel.Data;


namespace Hotel.MappingProfiles;

public class CountryMappingProfile : Profile
{

    public CountryMappingProfile()
    {
        CreateMap<Counrty,GetCountriesDTO>();
        CreateMap<Counrty,GetCountryDTO>();
        CreateMap<CreateCountryDTO,Counrty>();
    }
}
