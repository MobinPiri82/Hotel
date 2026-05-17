using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Contract;
using Hotel.Data;
using Hotel.DTOs.Hotel;
using Hotel.Result;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Services;

public class HotelServices(Context context,IMapper mapper) : IHotelServices

{
    public async Task<Result<IEnumerable<GetHotelsDto>>> GetHotelsDto()
    {
        var hotels = await context.hotels.
            ProjectTo<GetHotelsDto>(mapper.ConfigurationProvider)
           .ToListAsync();
        return  Result<IEnumerable<GetHotelsDto>>.Success(hotels)  ;
    }
    public async Task<Result<GetHotelDTO>> GetHotelDTO(int id)
    {
        var Selectedhotel = await context.hotels
        .Where(a => a.id == id)
        .Include(q => q.country).ProjectTo<GetHotelDTO>(mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(a => a.id == id);
        return Selectedhotel is null ? 
            Result<GetHotelDTO>.NotFound() :
            Result<GetHotelDTO>.Success(Selectedhotel);
           
    }
    public async Task<Result<GetHotelDTO?>> CreateHotelDto(CreateHotelDTO newHotel)
    {
        var Exist = await context.hotels.AnyAsync(a => a.Name == newHotel.Name);
        if (!Exist)
        {
            var hotel = mapper.Map<HotelInfo>(newHotel);
            await context.hotels.AddAsync(hotel);
            await context.SaveChangesAsync();
            var returnObj = mapper.Map<GetHotelDTO>(hotel);
            return Result<GetHotelDTO?>.Success(returnObj);
        }
        return Result<GetHotelDTO?>.Failuar();
    }
    public async Task<Result<UpdateHotelDTO?>> UpdateHotelDto(int Id, UpdateHotelDTO updatedHotel)
    {
        var update = await context.hotels.FirstOrDefaultAsync(h => h.id == Id);
        if (update == null)
        {
            return Result<UpdateHotelDTO?>.NotFound();
        }
       mapper.Map(updatedHotel,update);
       context.Entry(updatedHotel).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await hotelExistAsync(Id))
                return Result<UpdateHotelDTO?>.Failuar();
            else throw;
        }
        return Result<UpdateHotelDTO?>.Success(updatedHotel);

    }
    public async Task DeleteHotel(int id)
    {
        //throw new Exception("Delete Delete Delete Delete Delete");
        var delete = await context.hotels.FirstOrDefaultAsync(a => a.id == id);
        if (delete != null)
        {
            context.hotels.Remove(delete);
            await context.SaveChangesAsync();
        }
        else throw new KeyNotFoundException();
        
    }
    public async Task<bool> hotelExistAsync(int id)
    {
        return await context.hotels.AnyAsync(h => h.id == id);
    }
    public async Task<bool> hotelExistAsync(string name)
    {
        return await context.hotels.AnyAsync(h => h.Name == name);
    }

    //Task<GetHotelDTO> IHotelServices.GetHotelDTO(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //Task<GetHotelsDto?> IHotelServices.Post(CreateHotelDTO newHotel)
    //{
    //    throw new NotImplementedException();
    //}

    //Task<UpdateHotelDTO> IHotelServices.Put(int Id, UpdateHotelDTO updatedHotel)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<GetHotelsDto?> creatho(CreateHotelDTO newHotel)
    //{
    //    throw new NotImplementedException();
    //}

    //Task<GetHotelDTO> IHotelServices.GetHotelDTO(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //Task<IEnumerable<GetHotelsDto>> IHotelServices.GetHotelsDto()
    //{
    //    throw new NotImplementedException();
    //}

    //Task<GetHotelsDto?> IHotelServices.Post(CreateHotelDTO newHotel)
    //{
    //    throw new NotImplementedException();
    //}

    //Task<UpdateHotelDTO> IHotelServices.Put(int Id, UpdateHotelDTO updatedHotel)
    //{
    //    throw new NotImplementedException();
    //}
}