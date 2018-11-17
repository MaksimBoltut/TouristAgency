using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.Services
{
    public class FilterViewModel
    {
        public SelectList Services { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Service> service, int? id, string name)
        {
            service.Insert(0, new Service { Name = "Все", ListID = 0 });
            Services = new SelectList(service, "ID", "Name", id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
