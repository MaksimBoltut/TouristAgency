using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TouristAgency.Controllers
{
    public class VouchersController : Controller
    {
        private readonly AgencyContext context;
        private VoucherViewModel _vouchers = new VoucherViewModel
        {
            HotelName = "",
            TypeRestName = "",
            ClientFullName = "",
            EmployeeFullName = ""
        };

        public VouchersController(AgencyContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var voucherContext = context.Vouchers.Include(p => p.Hotel).Include(p => p.TypeRest).Include(p => p.Client).Include(p => p.Employee);
            VouchersViewModel vouchers = new VouchersViewModel
            {
                Vouchers = voucherContext.Take(50).ToList(),
                VoucherViewModel = _vouchers
            };
            return View(vouchers);
        }
    }
}