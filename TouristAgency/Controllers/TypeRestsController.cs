using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;

namespace TouristAgency.Controllers
{
    public class TypeRestsController : Controller
    {
        private AgencyContext context;

        public TypeRestsController(AgencyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var typerests = context.TypeRests.ToList();
            return View(typerests);
        }
    }
}