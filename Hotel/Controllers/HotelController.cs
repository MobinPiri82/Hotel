using Hotel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Hotel
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController(Hotelcontext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await context.hotels.ToListAsync();
            return Ok(hotels);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var Selectedhotel = context.hotels.FirstOrDefaultAsync(a => a.id == Id);
            if (Selectedhotel == null)
            {
                return BadRequest("Hotel Does not Exist");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HotelInfo newHotel)
        {
            var Exist = await context.hotels.AnyAsync();
            if (Exist == null)
            {
                context.hotels.Add(newHotel);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = newHotel.id }, newHotel);
            }
            return BadRequest("Hotel already exist");

        }
        [HttpPut]
        public async Task<IActionResult> Put(int Id, [FromBody] HotelInfo updatedHotel) 
        {
            var Selectedhotel = await context.hotels.FirstOrDefaultAsync(a => a.id == Id);
            if (Selectedhotel == null)
            {
                return NotFound();
            }
            Selectedhotel.Name = updatedHotel.Name;
            Selectedhotel.Address = updatedHotel.Address;
            Selectedhotel.Rate = updatedHotel.Rate;
            await context.SaveChangesAsync();
            return Ok();
        }
    }

}
