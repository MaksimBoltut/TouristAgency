using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;

namespace TouristAgency.Controllers
{
    public class ServicesController : Controller
    {
        private AgencyContext context;

        public ServicesController(AgencyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var services = context._Services.ToList();
            return View(services);
        }
    }
}