﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlightSite.DTOs
{
    public record BookDTO(
        [Required] Guid FlightId,
        [Required][EmailAddress][StringLength(100, MinimumLength = 3)] string PassengerEmail,
        [Required][Range(1,254)] byte NumberOfSeats
        );
}
