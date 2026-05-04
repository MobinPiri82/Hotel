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
public class HotelController(IHotelServices hotelServices) : ControllerBase
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
        var hotels = await hotelServices.GetHotelsDto();
        return Ok( hotels);
    }
    
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get(int Id) 
    {
        var hotel = await hotelServices.GetHotelDTO(Id);
        return Ok( hotel);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateHotelDTO newHotel)
    {
        var CreateHotel = await hotelServices.CreateHotelDto(newHotel);
        return Ok(CreateHotel);

    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> Put(int Id, [FromBody] UpdateHotelDTO updatedHotel) 
    {
        var updatingHotel = await hotelServices.UpdateHotelDto(Id, updatedHotel);
        return Ok( updatingHotel);
    }
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        await hotelServices.DeleteHotel(Id);
        return Ok();
    }
    
}
