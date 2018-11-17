using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;

namespace TouristAgency.Controllers
{
    public class ClientsController : Controller
    {
        private AgencyContext context;

        public ClientsController(AgencyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var clients = context.Clients.ToList();
            return View(clients);
        }
    }
}