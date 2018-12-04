using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.ServiceList
{
    public class ServiceListViewModel
    {
        [Display(Name = "Код листа")]
        public int ID { get; set; }

        [Display(Name = "Наименование сервиса")]
        public DateTime ServiceName { get; set; }

        [Display(Name = "Код путёвки")]
        public string VoucherID { get; set; }
    }
}
