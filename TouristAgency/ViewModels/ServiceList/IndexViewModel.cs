using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.ServiceList
{
    public class IndexViewModel
    {
        public IEnumerable<TouristAgency.Models.ServiceList> ServiceList { get; set; }
        public SelectList ServiceNames { get; set; }
        public ServiceListViewModel ServiceListViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
