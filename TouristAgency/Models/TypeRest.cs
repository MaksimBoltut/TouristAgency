﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class TypeRest
    {
        [Key]
        [Display(Name = "Код вида отдыха")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Ограничения")]
        public string Constraints { get; set; }
        public ICollection<Voucher> Vouchers { get; set; }
    }
}
