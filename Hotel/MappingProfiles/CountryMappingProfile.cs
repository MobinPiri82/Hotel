using AutoMapper;
using Hotel.DTOs.Country;
using Hotel.Data;


namespace Hotel.MappingProfiles;

public class CountryMappingProfile : Profile
{

    public CountryMappingProfile()
    {
        CreateMap<Counrty,GetCountriesDTO>();
        CreateMap<Counrty,GetCountryDTO>().ForMember(d => d.HotelNames ,cfg => cfg.MapFrom<HotelNameResolver>());
        CreateMap<CreateCountryDTO,Counrty>();
    }
}
public class HotelNameResolver(HotelInfo src, GetCountryDTO des, string desMember) : IValueResolver<HotelInfo, GetCountryDTO, string>
{
    public string Resolve(Counrty source, GetCountryDTO destination, string destMember, ResolutionContext context)
    {
        return src.
    }
}