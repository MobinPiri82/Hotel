using Hotel.Contract;
using Hotel.Data;
using Hotel.DTOs.Hotel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Services;

public class HotelServices(Context context) : IHotelServices

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
    public async Task<GetHotelDTO> GetHotelDTO(int id)
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
        if (Selectedhotel == null)
        {
            throw new Exception("Not Found");
        }
        
    }
    public async Task<GetHotelsDto?> CreateHotelDto(CreateHotelDTO newHotel)
    {
        var Exist = await context.hotels.AnyAsync(a => a.Name == newHotel.Name);
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
            var hotelDto = new GetHotelsDto(
        hotel.id,
        hotel.Name,
        hotel.Address,
        hotel.Rate,
        hotel.CountryId
    );
            return hotelDto;
            //return CreatedAtAction(nameof(Get), new { id = hotel.id }, hotel);
        }
        //return BadRequest("Hotel already exist");
        return null;
    }
    public async Task<UpdateHotelDTO?> UpdateHotelDto(int Id, UpdateHotelDTO updatedHotel)
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

        try
        {
            await context.SaveChangesAsync();
            context.Entry(updatedHotel).State = EntityState.Modified;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await hotelExistAsync(Id))
                return null;
            else throw;
        }
        return updatedHotel;

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