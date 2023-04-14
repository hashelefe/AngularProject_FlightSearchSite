using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSite.ReadModels
{
    public record FlightRm(
        Guid Id,
        string Airline,
        string Price,
        TimePlaceRM Departue,
        TimePlaceRM Arrival,
        int RemainingNumberOfSeats
        );

}
