using System;
using System.ComponentModel;

namespace FlightSite.DTOs
{
    public record FlightSearchParameters (
    [DefaultValue("12/25/2023 10:30:00 AM")]
     DateTime? FromDate,
    [DefaultValue("12/26/2023 10:30:00 AM")]
     DateTime? ToDate,
    [DefaultValue("Los Angeles")]
     string? Source,
    [DefaultValue("London")]
     string? Destination,
    [DefaultValue("1")]
     byte? NumberOfPassenger)
    {
    }
}
