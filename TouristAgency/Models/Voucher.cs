using TouristAgency.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class Voucher
    {
        [Key]
        [Display(Name = "Код путёвки")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Дата начала")]
        [DataType(DataType.Date)]
        public DateTime DateBeginning { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Дата окончания")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        public int HotelID { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        public int TypeRestID { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        public int EmployeeID { get; set; }
        
        [Display(Name = "Отметка о бронировании")]
        public bool BookingNote { get; set; }
        
        [Display(Name = "Отметка об оплате")]
        public bool PaymentNote { get; set; }

        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public Hotel Hotel { get; set; }
        public TypeRest TypeRest { get; set; }
        
        public ICollection<ServiceList> ServiceList { get; set; }
    }
}
