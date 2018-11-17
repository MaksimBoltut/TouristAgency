using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.TypeRests
{
    public class IndexViewModel
    {
        public IEnumerable<TypeRest> TypeRests { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
