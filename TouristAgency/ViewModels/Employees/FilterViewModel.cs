using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.Employees
{
    public class FilterViewModel
    {
        public SelectList Employees { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedFullName { get; private set; }
        public string SelectedPosition { get; private set; }
        public int? SelectedAge { get; private set; }

        public FilterViewModel(List<Employee> employee, int? id, string fullname, string position, int? age)
        {
            employee.Insert(0, new Employee { Fullname = "Все", ID = 0});
            Employees = new SelectList(employee, "ID", "FullName", id);
            SelectedId = id;
            SelectedFullName = fullname;
            SelectedPosition = position;
            SelectedAge = age;
        }
    }
}
