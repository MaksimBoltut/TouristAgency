using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [Range(1918, 2010)]
        public int Year { get; set; }
    }
}
