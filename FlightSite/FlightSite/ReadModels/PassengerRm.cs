using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSite.ReadModels
{
    public record PassengerRm(
        string Email,
        string FirstName,
        string LastName,
        bool Gender);
}
