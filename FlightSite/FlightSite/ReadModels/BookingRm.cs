using System;
 

namespace FlightSite.ReadModels
{
    public record BookingRm(
        Guid FlightId,
        string Airline,
        string Price,
        TimePlaceRM Departure,
        TimePlaceRM Arrival,
        int NumberOfBookedSeats,
        string PassengerEmail);
    
    
}
