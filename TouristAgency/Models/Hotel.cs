using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class Hotel
    {
        [Key]
        [Display(Name = "Код отеля")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Адрес")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Range(100000, 999999)]
        [Display(Name = "Телефон")]
        public int Telephone { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Range(1, 10)]
        [Display(Name = "Кол-во звёзд")]
        public int Stars { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Контактное лицо")]
        public string ContactFace { get; set; }
        
        [Display(Name = "Фото отеля")]
        public string FotoHotel { get; set; }
        public ICollection<Voucher> Vouchers { get; set; }
    }
}
