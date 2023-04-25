using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSite.DTOs;
using FlightSite.ReadModels;
using FlightSite.Domain.Entities;
using FlightSite.Data;

namespace FlightSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly Entities _entities;

        public PassengerController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public IActionResult Register(NewPassengerDTO dto)
        {
            _entities.Passengers.Add(new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender));

            _entities.SaveChanges();


            return CreatedAtAction(nameof(Find), new { email = dto.Email });
        }
        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(x => x.Email == email);
            if (passenger == null)
                return NotFound();

            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender);

            return Ok(rm);
        }
    }
}
