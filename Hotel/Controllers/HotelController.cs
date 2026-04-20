using Hotel.Data;
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
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var hotels = await Context.hotels.ToListAsync();
        return Ok(hotels);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        var Selectedhotel = Context.hotels.FirstOrDefaultAsync(a => a.id == Id);
        if (Selectedhotel == null)
        {
            return BadRequest("Hotel Does not Exist");
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] HotelInfo newHotel)
    {
        var Exist = await Context.hotels.AnyAsync();
        if (Exist == null)
        {
            Context.hotels.Add(newHotel);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newHotel.id }, newHotel);
        }
        return BadRequest("Hotel already exist");

    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> Put(int Id, [FromBody] HotelInfo updatedHotel) 
    {
        if (Id != updatedHotel.id)
        {
            return BadRequest();
        }
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
    private bool hotelExistAsync(int id)
    {
        return Context.hotels.Any(h => h.id == id);
    }
}
