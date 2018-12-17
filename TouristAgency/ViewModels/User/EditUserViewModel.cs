﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле не заполнено")]
        [Range(1918, 2010)]
        public int Year { get; set; }
    }
}
