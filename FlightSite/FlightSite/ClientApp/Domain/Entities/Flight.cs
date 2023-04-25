using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSite.Domain.Entities;
using FlightSite.Domain.Errors;



namespace FlightSite.Domain.Entities
{
  public class Flight
  {
    public Guid Id { get; set; }
    public string Airline { get; set; }
    public string Price { get; set; }
    public TimePlace Departure { get; set; }
    public TimePlace Arrival { get; set; }
    public int RemainingNumberOfSeats { get; set; }

    public IList<Booking> Bookings = new List<Booking>();

    public Flight(
      Guid id,
      string airline,
      string price,
      TimePlace departure,
      TimePlace arrival,
      int remainingNumberOfSeats)
    {
      Id = id;
      Airline = airline;
      Price = price;
      Departure = departure;
      Arrival = arrival;
      RemainingNumberOfSeats = remainingNumberOfSeats;
    }

    public Flight()
    {

    }

    public object? MakeBooking(string PassengerEmail, byte NumberOfSeats)
    {
      var flight = this;

      if (flight.RemainingNumberOfSeats < NumberOfSeats)
        return new OverbookError();

      flight.Bookings.Add(
          new Booking(
              this.Id,
              PassengerEmail,
              NumberOfSeats));

      flight.RemainingNumberOfSeats -= NumberOfSeats;
      return null;
    }

    public object? CancelBooking(string email, byte NumberOfSeats)
    {
      var booking = Bookings.FirstOrDefault(b => NumberOfSeats == b.NumberOfSeats && b.PassengerEmail.ToLower() == email.ToLower());

      if (booking == null)
        return new NotFoundError();

      Bookings.Remove(booking);
      RemainingNumberOfSeats += NumberOfSeats;
      return null;
    }


  }
}
    
 
