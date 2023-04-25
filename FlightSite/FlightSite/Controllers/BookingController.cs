using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightSite.Data;
using FlightSite.ReadModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using FlightSite.DTOs;
using FlightSite.Domain.Errors;

namespace FlightSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;
        public BookingController(Entities entities)
        {
            _entities = entities;
        }


        [HttpGet("{email}")]
        [ProducesResponseType(typeof(IEnumerable<BookingRm>),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<BookingRm>> List(string email)
        {
            var bookings = _entities.Flights.ToArray()
                .SelectMany(f => f.Bookings
                    .Where(b => b.PassengerEmail == email)
                        .Select(b => new BookingRm(
                            f.Id,
                            f.Airline,
                            f.Price.ToString(),
                            new TimePlaceRM(f.Arrival.Place, f.Arrival.Time),
                            new TimePlaceRM(f.Departure.Place, f.Departure.Time),
                            b.NumberOfSeats,
                            email
                            )));

            return Ok(bookings);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Cancel(BookDTO dto)
        {
            var flight = _entities.Flights.Find(dto.FlightId);
            var error = flight?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);
            if(error == null)
            {
                _entities.SaveChanges();
                return NoContent();
            }

            if (error is NotFoundError)
                return NotFound();

            throw new Exception($"The error of type: {error.GetType().Name} occured while canceling the booking");
        }
    }
}
