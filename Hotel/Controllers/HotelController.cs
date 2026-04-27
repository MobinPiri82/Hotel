using Hotel.Contract;
using Hotel.Data;
using Hotel.DTOs.Hotel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics.Eventing.Reader;

namespace Hotel;

//[Controller]
[Route("api/v1/Hotel")]
[ApiController]
public class HotelController(Hotelcontext Context) : ControllerBase
{

    //public HotelController(Hotelcontext context1)
    //{
    //    context = context1;
    //}
    //private readonly ILogger<HotelController> _logger;
    //public HotelController(ILogger<HotelController> logger)
    //{
    //    this._logger = logger;
    //}
        //var hotel = await Context.hotels.ToListAsync();
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotelsDto()
    {
        var hotels = await Context.countr
    }


        //var hotels = await Context.hotels.ToListAsync();
        //return Ok(hotels);
    
    [HttpGet("{Id}")]
    public async Task<ActionResult<GetHotelDTO>> Get(int Id) // IActionResult : Error
    {
        

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateHotelDTO newHotel)
    {
        var Exist = await Context.hotels.AnyAsync();
        if (!Exist)
        {
            var hotel = new HotelInfo
            {
                Name = newHotel.Name,
                Address = newHotel.Address,
                Rate = newHotel.Rate,
                CountryId = newHotel.CountryId

            };
            Context.hotels.Add(hotel);
            await Context.SaveChangesAsync();
            var hotelDto = new GetHotelsDto(
        hotel.id,
        hotel.Name,
        hotel.Address,
        hotel.Rate,
        hotel.CountryId
    );
            return CreatedAtAction(nameof(Get), new { id = hotel.id }, hotel);
        }
        return BadRequest("Hotel already exist");

    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> Put(int Id, [FromBody] UpdateHotelDTO updatedHotel) 
    {
        var update = await Context.hotels.FirstOrDefaultAsync(h=> h.id == Id);
        if (update == null)
        {
            return NotFound();
        }
        update.Name = updatedHotel.Name;
        update.Address = updatedHotel.Address;
        update.Rate= updatedHotel.Rate;
        update.CountryId= updatedHotel.CountryId;

        Context.Entry(updatedHotel).State = EntityState.Modified;
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) 
        {
            if (!hotelExistAsync(Id))
                return NotFound();
            else throw;
        }
        return NoContent();

        //var Selectedhotel = await context.hotels.FirstOrDefaultAsync(a => a.id == Id);
        //if (Selectedhotel == null)
        //{
        //    return NotFound();
        //}
        //Selectedhotel.Name = updatedHotel.Name;
        //Selectedhotel.Address = updatedHotel.Address;
        //Selectedhotel.Rate = updatedHotel.Rate;
        //await context.SaveChangesAsync();
        //return Ok();
    }
    
}
