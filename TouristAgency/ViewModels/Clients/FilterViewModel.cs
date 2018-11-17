using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.Models;

namespace TouristAgency.ViewModels.Clients
{
    public class FilterViewModel
    {
        public SelectList Clients { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedFullName { get; private set; }

        public FilterViewModel(List<Client> client, int? id, string fullname)
        {
            client.Insert(0, new Client { FullName = "Все", ID = 0 });
            Clients = new SelectList(client, "ID", "FullName", id);
            SelectedId = id;
            SelectedFullName = fullname;
        }
    }
}
