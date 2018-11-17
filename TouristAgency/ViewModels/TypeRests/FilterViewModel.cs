using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.TypeRests
{
    public class FilterViewModel
    {
        public SelectList TypeRests { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<TypeRest> typerest, int? id, string name)
        {
            typerest.Insert(0, new TypeRest { Name = "Все", ID = 0 });
            TypeRests = new SelectList(typerest, "ID", "Name", id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
