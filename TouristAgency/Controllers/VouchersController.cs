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
using TouristAgency.Infrastructure.Filters;
using TouristAgency.Infrastructure;
using Microsoft.AspNetCore.Authorization;

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
        
        [SetToSession("Vouchers")]
        public async Task<IActionResult> Index(int? id, string typerest, string hotel, DateTime datebeginning, DateTime expirationdate, int page = 0, SortState sortOrder = SortState.IdAsc)
        {
            var sessionVouchers = HttpContext.Session.Get("Vouchers");
            if (sessionVouchers != null && id == null && typerest == null && hotel == null && datebeginning == Convert.ToDateTime("01.01.0001")
                && expirationdate == Convert.ToDateTime("01.01.0001") && page == 0 && sortOrder == SortState.IdAsc)
            {
                try
                {
                    if (sessionVouchers.Keys.Contains("id"))
                        id = Convert.ToInt32(sessionVouchers["id"]);
                    if (sessionVouchers.Keys.Contains("typerest"))
                        typerest = sessionVouchers["typerest"];
                    if (sessionVouchers.Keys.Contains("hotel"))
                        hotel = sessionVouchers["hotel"];
                    if (sessionVouchers.Keys.Contains("datebeginning"))
                        datebeginning = Convert.ToDateTime(sessionVouchers["datebeginning"]);
                    if (sessionVouchers.Keys.Contains("expirationdate"))
                        expirationdate = Convert.ToDateTime(sessionVouchers["expirationdate"]);
                }
                catch { }
                if (sessionVouchers.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionVouchers["page"]);
                if (sessionVouchers.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionVouchers["sortOrder"]);
            }

            if (page == 0)
                page = 1;

            int pageSize = 10;

            IQueryable<Voucher> source = context.Vouchers.Include(p => p.Hotel).Include(o => o.TypeRest).
                Include(m => m.Client).Include(k => k.Employee).Include(o => o.ServiceList).ThenInclude(l => l.Service);

            if (id != null && id != 0)
                source = source.Where(p => p.ID == id);
            if (!String.IsNullOrEmpty(typerest))
                source = source.Where(p => p.TypeRest.Name == typerest);
            if (!String.IsNullOrEmpty(hotel))
                source = source.Where(p => p.Hotel.Name == hotel);
            if (datebeginning != Convert.ToDateTime("01.01.0001"))
                source = source.Where(p => p.DateBeginning == datebeginning);
            if (expirationdate != Convert.ToDateTime("01.01.0001"))
                source = source.Where(p => p.ExpirationDate == expirationdate);

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
                FilterViewModel = new FilterViewModel(context.Vouchers.ToList(), id, typerest, hotel, datebeginning, expirationdate)
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
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

            ViewBag.Services = context._Services;

            return View(voucher);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(Voucher voucher, int[] services) //Исправить
        {
            var serviceLists = context.ServiceList.Where(s => s.VoucherID == voucher.ID);
            context.ServiceList.RemoveRange(serviceLists);
            await context.SaveChangesAsync();

            foreach (int s in services)
            {
                context.ServiceList.Add(new ServiceList { ServiceID = s, VoucherID = voucher.ID });
            }
            context.Vouchers.Update(voucher);

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var voucher = context.Vouchers.FirstOrDefault(c => c.ID == id);
                context.Vouchers.Remove(voucher);
                await context.SaveChangesAsync();
            }
            catch { }
            return RedirectToAction("index");
        }

        [Authorize(Roles = "Admin")]
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
            ViewBag.Services = context._Services;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(Voucher voucher, int[] services)
        {
            List<ServiceList> list = new List<ServiceList>();

            foreach (int s in services)
            {
                list.Add(new ServiceList { ServiceID = s });
            }
            voucher.ServiceList = list;

            context.Vouchers.Add(voucher);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}