using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.Vouchers
{
    public class FilterViewModel
    {
        public SelectList Vouchers { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedTypeRest { get; private set; }
        public string SelectedHotel { get; set; }
        public DateTime SelectedDateBeginning { get; private set; }
        public DateTime SelectedExpirationDate { get; private set; }


        public FilterViewModel(List<Voucher> voucher, int? id, string typerest, string hotel, DateTime datebeginning, DateTime expirationdate)
        {
            voucher.Insert(0, new Voucher { ID = 0 });
            Vouchers = new SelectList(voucher, "ID");
            SelectedId = id;
            SelectedTypeRest = typerest;
            SelectedHotel = hotel;
            SelectedDateBeginning = datebeginning;
            SelectedExpirationDate = expirationdate;
        }
    }
}
