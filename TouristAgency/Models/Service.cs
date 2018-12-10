using TouristAgency.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class Service
    {
        [Key]
        [Display(Name = "Код сервиса")]
        public int ListID { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Range(1, 100000)]
        [Display(Name = "Цена")]
        public int Price { get; set; }
        public ICollection<ServiceList> ServiceList { get; set; }
    }
}
