using FlightSite.ReadModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSite.DTOs;
using FlightSite.Domain.Entities;
using FlightSite.Domain.Errors;
using FlightSite.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly Entities _entities;
        private readonly ILogger<FlightController> _logger;
       
        public FlightController(ILogger<FlightController> logger, Entities entities)
        {
            _logger = logger;
            this._entities = entities;
        }


    [HttpGet]
    public IEnumerable<FlightRm> Search([FromQuery] FlightSearchParameters @params)
        {
            _logger.LogInformation("Searching for a flight to {Destination}", @params.Destination);

            IQueryable<Flight> flights = _entities.Flights;
            if(!string.IsNullOrWhiteSpace(@params.Destination)) 
                flights = flights.Where(f => f.Arrival.Place.Contains(@params.Destination));
            
            if(!string.IsNullOrWhiteSpace(@params.Source)) 
                flights = flights.Where(f => f.Departure.Place.Contains(@params.Source));    
            
            if(@params.FromDate != null)
                flights=flights.Where(f=>f.Departure.Time >=  @params.FromDate.Value.Date);
            
            if(@params.ToDate != null)
                flights=flights.Where(f=>f.Departure.Time >=  @params.ToDate.Value.Date.AddDays(1).AddTicks(-1));

            if (@params.NumberOfPassenger != null && @params.NumberOfPassenger != 0)
                flights = flights.Where(f => f.RemainingNumberOfSeats >= @params.NumberOfPassenger);
            else
                flights = flights.Where(f => f.RemainingNumberOfSeats >= 1);



            var flightRm = _entities.Flights
                .Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRM(
                    flight.Departure.Place.ToString(),
                    flight.Departure.Time),
                new TimePlaceRM(
                    flight.Arrival.Place.ToString(),
                    flight.Arrival.Time),
                flight.RemainingNumberOfSeats));

            return flightRm;
        }

        [HttpGet("{id}")]
        public ActionResult<FlightRm> Find(Guid id)
        {
           var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
                return NotFound();

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRM(
                    flight.Departure.Place.ToString(),
                    flight.Departure.Time),
                new TimePlaceRM(
                    flight.Arrival.Place.ToString(),
                    flight.Arrival.Time),
                flight.RemainingNumberOfSeats);

            return Ok(readModel);

        }
      
        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public IActionResult Book(BookDTO dto)
        {
            System.Diagnostics.Debug.WriteLine("Booking new flight:", dto.FlightId);
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null)
                return NotFound();

            var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);
          

            if (error is OverbookError)
                return Conflict(new { message = "Not enough seats." });
            
            try { _entities.SaveChanges(); }
            catch (DbUpdateConcurrencyException e)
            { return Conflict(new { message = "An error occured while booking." });  }
            

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });

        }
    }
}
