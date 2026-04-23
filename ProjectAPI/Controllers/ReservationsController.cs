using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Models;

namespace ProjectAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] DateOnly? date, [FromQuery] string? status, [FromQuery] int? roomId)
    {
        var reservations = DataStorage.Reservations.AsQueryable();
        if(date != null) reservations = reservations.Where(r => r.Date == date.Value);
        if(status != null) reservations = reservations.Where(r => r.Status == status);
        if(roomId != null) reservations = reservations.Where(r => r.RoomId == roomId);
        return Ok(reservations);
    }

    [Route("{id}")]
    [HttpGet]
    public IActionResult GetById([FromRoute] int id)
    {
        var reservation = DataStorage.Reservations.FirstOrDefault(r => r.Id == id);
        if(reservation == null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Reservation newReservation)
    {
        var existingReservation = DataStorage.Reservations.AsQueryable();
        var room = DataStorage.Rooms.FirstOrDefault(r => r.Id == newReservation.RoomId);
        if (room == null) return NotFound();
        if (!room.IsActive) return BadRequest();
        if (existingReservation
            .Any(r => r.Date == newReservation.Date 
                      && r.StartTime <= newReservation.EndTime
                      && r.EndTime >= newReservation.StartTime)) return Conflict();
        
        newReservation.Id = DataStorage.Reservations.Max(r => r.Id) + 1;
        DataStorage.Reservations.Add(newReservation);
        return CreatedAtAction(nameof(GetById), new { id = newReservation.Id }, newReservation);
    }

    [Route("{id}")]
    [HttpPut]
    public IActionResult Update([FromRoute] int id, [FromBody] Reservation updatedReservation)
    {
        var exisitingReservation = DataStorage.Reservations.AsQueryable();
        var room = DataStorage.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);
        var currentReservation = DataStorage.Reservations.FirstOrDefault(r => r.Id == id);
        if (currentReservation == null) return NotFound();
        if (room == null) return NotFound();
        if (!room.IsActive)  return BadRequest();
        if(exisitingReservation
           .Any(r => r.Id != id 
                            && r.Date == updatedReservation.Date
                    && r.StartTime <= updatedReservation.EndTime
                    && r.EndTime >= updatedReservation.StartTime
                    && r.RoomId == updatedReservation.RoomId)) return Conflict();
        
        currentReservation.RoomId = updatedReservation.RoomId;
        currentReservation.OrganizerName = updatedReservation.OrganizerName;
        currentReservation.Topic = updatedReservation.Topic;
        currentReservation.Date = updatedReservation.Date;
        currentReservation.StartTime = updatedReservation.StartTime;
        currentReservation.EndTime = updatedReservation.EndTime;
        currentReservation.Status = updatedReservation.Status;
        
        return Ok(currentReservation);
    }

    [Route("{id}")]
    [HttpDelete]
    public IActionResult Delete([FromRoute] int id)
    {
        var reservation = DataStorage.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null) return NotFound();
        DataStorage.Reservations.Remove(reservation);
        return NoContent();
    }
    
}