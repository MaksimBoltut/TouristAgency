using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels
{
    public class VoucherViewModel
    {
        [Display(Name = "Код путёвки")]
        public int ID { get; set; }

        [Display(Name = "Дата начала")]
        [DataType(DataType.Date)]
        public DateTime DateBeginning { get; set; }

        [Display(Name = "Дата окончания")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Название отеля")]
        public string HotelName { get; set; }

        [Display(Name = "Название типа отдыха")]
        public string TypeRestName { get; set; }

        [Display(Name = "ФИО клиента")]
        public string ClientFullName { get; set; }

        [Display(Name = "ФИО работника")]
        public string EmployeeFullName { get; set; }

        [Display(Name = "Отметка о бронировании")]
        public bool BookingNote { get; set; }

        [Display(Name = "Отметка оп оплате")]
        public bool PaymentNote { get; set; }
    }
}
