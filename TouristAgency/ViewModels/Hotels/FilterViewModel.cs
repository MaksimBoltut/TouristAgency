using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.Hotels
{
    public class FilterViewModel
    {
        public SelectList Hotels { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Hotel> hotel, int? id, string name)
        {
            hotel.Insert(0, new Hotel { Name = "Все", ID = 0 });
            Hotels = new SelectList(hotel, "ID", "Name", id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
