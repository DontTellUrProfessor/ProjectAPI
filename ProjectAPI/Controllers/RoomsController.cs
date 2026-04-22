using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var rooms = DataStorage.Rooms.AsQueryable();
            if (minCapacity != null) rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);
            if(hasProjector != null) rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
            if(activeOnly != null) rooms = rooms.Where(r => r.IsActive == activeOnly.Value);
            return Ok(rooms);
        }

        [Route("building/{BuildingCode}")]
        [HttpGet]
        public IActionResult GetByBuilding([FromRoute] string BuildingCode)
        {
            var rooms = DataStorage.Rooms.Where(r => r.BuildingCode == BuildingCode);
            return Ok(rooms);
        }
        
        
        //GET api/rooms/{id}
        [Route("{id}")]
        [HttpGet]
        public IActionResult Get([FromRoute]int id)
        {
            var room = DataStorage.Rooms.FirstOrDefault(r => r.Id == id);
            if(room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Room room)
        {
            room.Id = DataStorage.Rooms.Max(r => r.Id) + 1;
            DataStorage.Rooms.Add(room);
            return CreatedAtAction(nameof(Get), new { id = room.Id }, room);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] int Id, [FromBody] Room room)
        {
            var currentRoom = DataStorage.Rooms.FirstOrDefault(r => r.Id == Id);
            if(currentRoom == null) return NotFound();
            currentRoom.Name = room.Name;
            currentRoom.BuildingCode =  room.BuildingCode;
            currentRoom.Floor = room.Floor;
            currentRoom.Capacity = room.Capacity;
            currentRoom.IsActive = room.IsActive;
            currentRoom.HasProjector =  room.HasProjector;

            return Ok(currentRoom);
        }
    }
}
