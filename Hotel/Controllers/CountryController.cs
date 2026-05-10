using AutoMapper;
using Hotel.Contract;
using Hotel.Data;
using Hotel.DTOs.Country;
using Hotel.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Controllers;

[Route("api/country")]
[ApiController]
public class CountryController(ICountryInterface countryServices) : ControllerBase
{
    // GET: api/<CountryController>
    [HttpGet("GetCountries")]
    public async Task<IActionResult> GetCountries()
    {
        var getcountries =await countryServices.GetCountries();
        return Ok(getcountries);
    }

    // GET api/<CountryController>/5
    [HttpGet("GetCountry{id}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        var getcountry = await countryServices.GetCountry(id);
        return Ok(getcountry);
    }

    // POST api/<CountryController>
    [HttpPost("CreateCountry")]
    public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO newCountry)
    {
        var createCountry = await countryServices.CreateCountry(newCountry);
        return Ok(createCountry);
    }

    // PUT api/<CountryController>/5
    [HttpPut("UpdateCountry{id}")]
    public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO updatingHotel)
    {
        var update = await countryServices.UpdateCountry(id,updatingHotel);
        return Ok(update);
    }

    // DELETE api/<CountryController>/5
    [HttpDelete("DeleteCountry{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await countryServices.DeleteCountry(id);
        return Ok();
        
    }
}
