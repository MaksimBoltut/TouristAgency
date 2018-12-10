﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class Client
    {
        [Key]
        [Display(Name = "Код клиента")]
        public int ID { get; set; }
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }
        [StringLength(1)]
        [Display(Name = "Пол")]
        public string Sex { get; set; }
        [Display(Name = "Адрес")]
        public string Adress { get; set; }
        [Range(100000, 999999)]
        [Display(Name = "Телефон")]
        public int Telephone { get; set; }
        [Display(Name = "Пасспортные данные")]
        public string PassData { get; set; }
        [Display(Name = "Скидка")]
        public int Discount { get; set; }
        public ICollection<Voucher> Vouchers { get; set; }
    }
}
