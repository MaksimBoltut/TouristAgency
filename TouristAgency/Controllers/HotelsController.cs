using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;

namespace TouristAgency.Controllers
{
    public class HotelsController : Controller
    {
        private AgencyContext context;

        public HotelsController(AgencyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var hotels = context.Hotels.ToList();
            return View(hotels);
        }
    }
}