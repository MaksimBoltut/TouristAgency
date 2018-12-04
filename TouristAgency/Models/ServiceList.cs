using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class ServiceList
    {
        [Key]
        public int ID { get; set; }
        public int ServiceID { get; set; }
        public int VoucherID { get; set; }
        public Voucher Voucher { get; set; }
        public Service Service { get; set; }
    }
}
