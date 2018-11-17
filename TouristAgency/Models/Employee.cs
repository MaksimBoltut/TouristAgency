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
        [Display(Name = "ФИО")]
        public string Fullname { get; set; }
        [Display(Name = "Должность")]
        public string Position { get; set; }
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }
        public ICollection<Voucher> Vouchers { get; set; }
    }
}
