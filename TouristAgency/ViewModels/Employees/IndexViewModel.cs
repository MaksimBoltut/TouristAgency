using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.Employees
{
    public class IndexViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
