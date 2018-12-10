using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "Код работника")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "ФИО")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Range(18, 100)]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Range(100000, 999999)]
        [Display(Name = "Телефон")]
        public int Telephone { get; set; }
        public ICollection<Voucher> Vouchers { get; set; }
    }
}
