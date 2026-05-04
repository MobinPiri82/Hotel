using Hotel.Contract;
using Hotel.Data;
using Hotel.DTOs.Country;
using Hotel.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController(ICountryInterface countryServices) : ControllerBase
{
    // GET: api/<CountryController>
    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        var gethotels =await countryServices.GetCountries();
        return Ok(gethotels);
    }

    // GET api/<CountryController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        var gethotel = await countryServices.GetCountry(id);
        return Ok(gethotel);
    }

    // POST api/<CountryController>
    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO newCountry)
    {
        var createCountry = await countryServices.CreateCountry(newCountry);
        return Ok(createCountry);
    }

    // PUT api/<CountryController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO updatingHotel)
    {
        var update = await countryServices.UpdateCountry(id,updatingHotel);
        return Ok(update);
    }

    // DELETE api/<CountryController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await countryServices.DeleteCountry(id);
        return Ok();
        
    }
}
