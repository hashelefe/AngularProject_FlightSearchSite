using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSite.Domain.Entities
{
    public record Passenger(
        string Email,
        string FirstName,
        string LastName,
        bool Gender);
}
