using TouristAgency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels
{
    public class VouchersViewModel
    {
        public IEnumerable<Voucher> Vouchers { get; set; }
        public VoucherViewModel VoucherViewModel { get; set; }
    }
}
