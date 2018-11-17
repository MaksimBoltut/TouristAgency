using TouristAgency.Models;
using TouristAgency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Controllers
{
    public class EmployeesController : Controller
    {
        private AgencyContext context;

        public EmployeesController(AgencyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var employees = context.Employees.ToList();
            return View(employees);
        }
    }
}
