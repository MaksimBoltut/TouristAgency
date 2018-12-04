using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.ServiceList
{
    public class FilterViewModel
    {
        public SelectList ServiceList { get; private set; }
        public int? SelectedId { get; private set; }

        public FilterViewModel(List<TouristAgency.Models.ServiceList> sl, int? id)
        {
            sl.Insert(0, new TouristAgency.Models.ServiceList { VoucherID = 0 });
            ServiceList = new SelectList(sl, "VoucherID");
            SelectedId = id;
        }
    }
}
