using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels.Vouchers;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Index(int? id, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;

            IQueryable<Voucher> source = context.Vouchers.Include(p => p.Hotel).Include(o => o.TypeRest).
                Include(m => m.Client).Include(k => k.Employee).Include(o => o.ServiceList).ThenInclude(l => l.Service);

            if (id != null && id != 0)
                source = source.Where(p => p.ID == id);

            switch (sortOrder)
            {
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.ID);
                    break;
                case SortState.DateBeginningDesc:
                    source = source.OrderByDescending(s => s.DateBeginning);
                    break;
                case SortState.DateBeginningAsc:
                    source = source.OrderBy(s => s.DateBeginning);
                    break;
                case SortState.ExpirationDateDesc:
                    source = source.OrderByDescending(s => s.ExpirationDate);
                    break;
                case SortState.ExpirationDateAsc:
                    source = source.OrderBy(s => s.ExpirationDate);
                    break;
                case SortState.HotelIDDesc:
                    source = source.OrderByDescending(s => s.Hotel.Name);
                    break;
                case SortState.HotelIDAsc:
                    source = source.OrderBy(s => s.Hotel.Name);
                    break;
                case SortState.TypeRestIDDesc:
                    source = source.OrderByDescending(s => s.TypeRest.Name);
                    break;
                case SortState.TypeRestIDAsc:
                    source = source.OrderBy(s => s.TypeRest.Name);
                    break;
                case SortState.ClientIDDesc:
                    source = source.OrderByDescending(s => s.Client.FullName);
                    break;
                case SortState.ClientIDAsc:
                    source = source.OrderBy(s => s.Client.FullName);
                    break;
                case SortState.EmployeeIDDesc:
                    source = source.OrderByDescending(s => s.Employee.Fullname);
                    break;
                case SortState.EmployeeIDAsc:
                    source = source.OrderBy(s => s.Employee.Fullname);
                    break;
                case SortState.BookingNoteDesc:
                    source = source.OrderByDescending(s => s.BookingNote);
                    break;
                case SortState.BookingNoteAsc:
                    source = source.OrderBy(s => s.BookingNote);
                    break;
                case SortState.PaymentNoteDesc:
                    source = source.OrderByDescending(s => s.PaymentNote);
                    break;
                case SortState.PaymentNoteAsc:
                    source = source.OrderBy(s => s.PaymentNote);
                    break;

                default:
                    source = source.OrderBy(s => s.ID);
                    break;
            }

            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Vouchers = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(context.Vouchers.ToList(), id)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var voucherContext = context.Vouchers.Include(p => p.Hotel).Include(p => p.TypeRest).Include(p => p.Client).Include(p => p.Employee)
                .Include(o => o.ServiceList).ThenInclude(i => i.Service);
            var items = voucherContext.Where(p => p.ID == id).ToList();
            var hotel = new SelectList(context.Hotels, "ID", "Name", items.First().HotelID);
            var typerest = new SelectList(context.TypeRests, "ID", "Name", items.First().TypeRestID);
            var client = new SelectList(context.Clients, "ID", "FullName", items.First().ClientID);
            var employee = new SelectList(context.Employees, "ID", "Fullname", items.First().EmployeeID);
            var service = new SelectList(context._Services, "ListID", "Name");
            IndexViewModel voucher = new IndexViewModel
            {
                Vouchers = items,
                HotelList = hotel,
                TypeRestList = typerest,
                ClientList = client,
                EmployeeList = employee,
                ServiceList = service,
                VoucherViewModel = _vouchers,
            };
            return View(voucher);
        }

        [HttpPost]
        public ActionResult Edit(Voucher voucher)
        {
            context.Vouchers.Update(voucher);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var voucherContext = context.Vouchers.Include(p => p.Hotel).Include(p => p.TypeRest).Include(p => p.Client).Include(p => p.Employee)
                .Include(o => o.ServiceList).ThenInclude(i => i.Service);
            var items = voucherContext.Where(p => p.ID == id).ToList();
            var hotel = new SelectList(context.Hotels, "ID", "Name");
            var typerest = new SelectList(context.TypeRests, "ID", "Name");
            var client = new SelectList(context.Clients, "ID", "FullName");
            var employee = new SelectList(context.Employees, "ID", "Fullname");
            var service = new SelectList(context._Services, "ID", "Name");
            VoucherViewModel voucherView = new VoucherViewModel
            {
                ID = items.First().ID,
                DateBeginning = items.First().DateBeginning,
                ExpirationDate = items.First().ExpirationDate,
                HotelName = items.First().Hotel.Name,
                TypeRestName = items.First().TypeRest.Name,
                ClientFullName = items.First().Client.FullName,
                EmployeeFullName = items.First().Employee.Fullname,
                BookingNote = items.First().BookingNote,
                PaymentNote = items.First().PaymentNote
            };
            IndexViewModel voucher = new IndexViewModel
            {
                Vouchers = items,
                HotelList = hotel,
                TypeRestList = typerest,
                ClientList = client,
                EmployeeList = employee,
                VoucherViewModel = voucherView,
                ServiceList = service
            };
            if (items == null)
                return View("NotFound");
            else
                return View(voucher);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var voucher = context.Vouchers.FirstOrDefault(c => c.ID == id);
                context.Vouchers.Remove(voucher);
                context.SaveChanges();
            }
            catch { }
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var hotel = new SelectList(context.Hotels, "ID", "Name");
            var typerest = new SelectList(context.TypeRests, "ID", "Name");
            var client = new SelectList(context.Clients, "ID", "FullName");
            var employee = new SelectList(context.Employees, "ID", "Fullname");
            var servicelist = new SelectList(context._Services, "ID", "Name");
            ViewBag.HotelID = hotel;
            ViewBag.ClientID = client;
            ViewBag.TypeRestID = typerest;
            ViewBag.EmployeeID = employee;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Voucher voucher)
        {
            context.Vouchers.Add(voucher);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}