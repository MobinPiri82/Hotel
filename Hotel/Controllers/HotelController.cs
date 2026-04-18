using Hotel.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private static List<HotelInfo> hotels = new List<HotelInfo>
        {
            new HotelInfo {id = 1,Name =  "Espinas",Address = "Saadat",Rate = 10.2},
            new HotelInfo {id = 2,Name =  "Esteghlal",Address = "Tehran",Rate = 14.5}
        };
        // GET: api/<HotelController>
        [HttpGet]
        public ActionResult<IEnumerable<HotelInfo>> Get()
        {
            return Ok(hotels);
        }

        // GET api/<HotelController>/5
        [HttpGet("{id}")]
        public ActionResult<HotelInfo> Get(int id)
        {
            var SelectedHotel = hotels.FirstOrDefault(h => h.id == id);
            if (SelectedHotel == null)
            {
                return NotFound();
            }
            return SelectedHotel;
        }

        // POST api/<HotelController>
        [HttpPost]
        public ActionResult<HotelInfo> Post([FromBody] HotelInfo newHotel )
        {
            if (hotels.Any(h => h.id == newHotel.id))
            {
                return BadRequest("This Hotel Already Exist");
            }
            else
                hotels.Add(newHotel);
            return CreatedAtAction(nameof(Get) , new {id = newHotel.id},newHotel);
        }

        // PUT api/<HotelController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] HotelInfo UpdatedHotel)
        {
            var exist = hotels.FirstOrDefault(h=>h.id == id);
            if(exist == null) 
                return NotFound();
            exist.id = UpdatedHotel.id;
            exist.Name = UpdatedHotel.Name;
            exist.Address = UpdatedHotel.Address;
            exist.Rate = UpdatedHotel.Rate;
            return NoContent();
        }

        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var exist = hotels.FirstOrDefault(h=>h.id==id);
            if(exist == null)
                return NotFound(new {message = "not found" });
            hotels.Remove(exist);
            return NoContent();
        }
    }
}
