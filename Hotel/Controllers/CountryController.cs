using Hotel.Data;
using Hotel.DTOs.Country;
using Hotel.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController(Hotelcontext context) : ControllerBase
{
    // GET: api/<CountryController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDTO>>> GetCountriesDTO()
    {
        var hotel = await context.countries.
            Select(h => new GetCountriesDTO(
                h.Name,
                h.Shortname
                )).ToListAsync();
        return Ok(hotel);
    }

    // GET api/<CountryController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDTO>> GetCountryDTO(int id)
    {
        var slectedHotel = await context.countries
            .Include(a=>a.Hotels)
            .Where(q => q.CountryId == id)
            .Select(c => new GetCountryDTO(c.CountryId,c.Name, c.Shortname, c.Hotels.Select(x => x.Name).ToList()))
            .ToListAsync();
        return Ok(slectedHotel);
    }

    // POST api/<CountryController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<CountryController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CountryController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
