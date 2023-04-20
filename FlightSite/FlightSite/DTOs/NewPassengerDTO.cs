using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlightSite.DTOs
{
    public record NewPassengerDTO(
        [Required] [EmailAddress] [StringLength(100,MinimumLength =3)] string Email,
        [Required] [MinLength(2)][MaxLength(45)]  string FirstName,
        [Required] [MinLength(2)][MaxLength(45)] string LastName,
        [Required] bool Gender)
    {
        

    }
}
