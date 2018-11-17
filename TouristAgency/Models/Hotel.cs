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
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Display(Name = "Город")]
        public string City { get; set; }
        [Display(Name = "Адрес")]
        public string Adress { get; set; }
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }
        [Display(Name = "Кол-во звёзд")]
        public int Stars { get; set; }
        [Display(Name = "Контактное лицо")]
        public string ContactFace { get; set; }
        [Display(Name = "Фото отеля")]
        public string FotoHotel { get; set; }
        public ICollection<Voucher> Vouchers { get; set; }
    }
}
