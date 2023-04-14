using FlightSite.ReadModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {

        private readonly ILogger<FlightController> _logger;

        static Random random = new Random();

        static private FlightRm[] flights = new FlightRm[]
            {
                 new (   Guid.NewGuid(),
                "American Airlines",
                random.Next(90, 5000).ToString(),
                new TimePlaceRM("Los Angeles",DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlaceRM("Istanbul",DateTime.Now.AddHours(random.Next(4, 10))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Deutsche BA",
                random.Next(90, 5000).ToString(),
                new TimePlaceRM("Munchen",DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlaceRM("Schiphol",DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "British Airways",
                random.Next(90, 5000).ToString(),
                new TimePlaceRM("London, England",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRM("Vizzola-Ticino",DateTime.Now.AddHours(random.Next(4, 18))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Basiq Air",
                random.Next(90, 5000).ToString(),
                new TimePlaceRM("Amsterdam",DateTime.Now.AddHours(random.Next(1, 21))),
                new TimePlaceRM("Glasgow, Scotland",DateTime.Now.AddHours(random.Next(4, 21))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "BB Heliag",
                random.Next(90, 5000).ToString(),
                new TimePlaceRM("Zurich",DateTime.Now.AddHours(random.Next(1, 23))),
                new TimePlaceRM("Baku",DateTime.Now.AddHours(random.Next(4, 25))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Adria Airways",
                random.Next(90, 5000).ToString(),
                new TimePlaceRM("Ljubljana",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRM("Warsaw",DateTime.Now.AddHours(random.Next(4, 19))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "ABA Air",
                random.Next(90, 5000).ToString(),
                new TimePlaceRM("Praha Ruzyne",DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlaceRM("Paris",DateTime.Now.AddHours(random.Next(4, 58))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "AB Corporate Aviation",
                random.Next(90, 5000).ToString(),
                new TimePlaceRM("Le Bourget",DateTime.Now.AddHours(random.Next(1, 58))),
                new TimePlaceRM("Zagreb",DateTime.Now.AddHours(random.Next(4, 60))),
                    random.Next(1, 853))
            };
       
        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }


    [HttpGet]
    public IEnumerable<FlightRm> Search()
         => flights;

        [HttpGet("{id}")]
        public ActionResult<FlightRm> Find(Guid id)
        {
           var flight = flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
                return NotFound();

            return Ok(flight);
        }
      

    }
}
