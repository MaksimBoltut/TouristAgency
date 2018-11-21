using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.Vouchers
{
    public class IndexViewModel
    {
        public IEnumerable<Voucher> Vouchers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SelectList ClientList { get; set; }
        public SelectList TypeRestList { get; set; }
        public SelectList EmployeeList { get; set; }
        public SelectList HotelList { get; set; }
        public SelectList ServiceList { get; set; }
        public VoucherViewModel VoucherViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
