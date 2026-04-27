using Hotel.Contract;
using Hotel.Data;
using Hotel.DTOs.Hotel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Services;

public class HotelServices(Hotelcontext context) : IHotelServices
{
    public async Task<IEnumerable<GetHotelsDto>> GetHotelsDto()
    {
        var hotels = await context.hotels
            .Select(h => new GetHotelsDto(
            h.id,
            h.Name,
            h.Address,
            h.Rate,
            h.CountryId)).ToListAsync();
        return (hotels);
    }
    public async Task<GetHotelDTO> GetHotelDTO(int id, Task<GetHotelDTO?> selectedHotel)
    {
        var Selectedhotel = context.hotels.
        Include(q => q.country).
        Select(h => new GetHotelDTO(
            h.id,
            h.Name,
            h.Address,
            h.Rate,
            h.country!.Shortname)).FirstOrDefaultAsync(a => a.id == id);
        return await Selectedhotel;
        //if (Selectedhotel == null)
        //{
        //    return BadRequest("Hotel Does not Exist");
        //}
        //else return Ok(Selectedhotel);
    }
    public async Task<GetHotelsDto?> Post(CreateHotelDTO newHotel)
    {
        var Exist = await context.hotels.AnyAsync(a=> a.Name == newHotel.Name);
        if (!Exist)
        {
            var hotel = new HotelInfo
            {
                Name = newHotel.Name,
                Address = newHotel.Address,
                Rate = newHotel.Rate,
                CountryId = newHotel.CountryId

            };
            context.hotels.Add(hotel);
            await context.SaveChangesAsync();
            var hotelDto =  new GetHotelsDto(
        hotel.id,
        hotel.Name,
        hotel.Address,
        hotel.Rate,
        hotel.CountryId
    );
            return  hotelDto;
            //return CreatedAtAction(nameof(Get), new { id = hotel.id }, hotel);
        }
        //return BadRequest("Hotel already exist");
        return null ;
    }
    public async Task<UpdateHotelDTO?> Put(int Id, UpdateHotelDTO updatedHotel)
    {
        var update = await context.hotels.FirstOrDefaultAsync(h => h.id == Id);
        if (update == null)
        {
            return null;
        }
        update.Name = updatedHotel.Name;
        update.Address = updatedHotel.Address;
        update.Rate = updatedHotel.Rate;
        update.CountryId = updatedHotel.CountryId;

        context.Entry(updatedHotel).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!hotelExistAsync(Id))
                return null;
            else throw;
        }
        return updatedHotel;

    }
    public async Task DeleteHotel(int id)
    {
        var delete = await context.hotels.FirstOrDefaultAsync(a => a.id == id);
        if (delete != null)
        {
            context.hotels.Remove(delete);
            await context.SaveChangesAsync();
        }
        else throw new KeyNotFoundException();

    }
    private bool hotelExistAsync(int id)
    {
        return context.hotels.Any(h => h.id == id);
    }

    Task<GetHotelDTO> IHotelServices.GetHotelDTO(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<GetHotelsDto>> IHotelServices.GetHotelsDto()
    {
        throw new NotImplementedException();
    }

    Task<GetHotelsDto?> IHotelServices.Post(CreateHotelDTO newHotel)
    {
        throw new NotImplementedException();
    }

    Task<UpdateHotelDTO> IHotelServices.Put(int Id, UpdateHotelDTO updatedHotel)
    {
        throw new NotImplementedException();
    }
}