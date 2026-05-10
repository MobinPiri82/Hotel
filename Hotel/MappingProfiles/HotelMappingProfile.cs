using AutoMapper;
using Hotel.DTOs.Hotel;
using Hotel.Data;


namespace Hotel.MappingProfiles;

public class HotelMappingProfile : Profile
{
    public HotelMappingProfile()
    {
        CreateMap<HotelInfo,GetHotelDTO>().
            ForMember(d => d.Country, cfg => cfg.MapFrom<CountryNameResolver>()); 
        CreateMap<CreateHotelDTO,HotelInfo>(); 
    }
}
public class CountryNameResolver : IValueResolver<HotelInfo, GetHotelDTO, string>
{
    public string Resolve(HotelInfo source, GetHotelDTO destination, string destMember, ResolutionContext context)
    {
        return source.country?.Name ?? string.Empty;
    }
}
