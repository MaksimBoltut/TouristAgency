using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels.Clients;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels;

namespace TouristAgency.Controllers
{
    public class ClientsController : Controller
    {
        private AgencyContext context;

        public ClientsController(AgencyContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int? id, string fullname, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;

            IQueryable<Client> source = context.Clients;

            if (id != null && id != 0)
            {
                source = source.Where(p => p.ID == id);
            }
            if (!String.IsNullOrEmpty(fullname))
            {
                source = source.Where(p => p.FullName.Contains(fullname));
            }

            switch (sortOrder)
            {
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.ID);
                    break;
                case SortState.FullNameDesc:
                    source = source.OrderByDescending(s => s.FullName);
                    break;
                case SortState.FullNameAsc:
                    source = source.OrderBy(s => s.FullName);
                    break;
                case SortState.DateBirthDesc:
                    source = source.OrderByDescending(s => s.DateBirth);
                    break;
                case SortState.DateBirthAsc:
                    source = source.OrderBy(s => s.DateBirth);
                    break;
                case SortState.DiscountDesc:
                    source = source.OrderByDescending(s => s.Discount);
                    break;
                case SortState.DiscountAsc:
                    source = source.OrderBy(s => s.Discount);
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
                Clients = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(context.Clients.ToList(), id, fullname)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Client client = context.Clients.Find(id);
            return View(client);
        }

        [HttpPost]
        public ActionResult Edit(Client client)
        {
            context.Clients.Update(client);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            Client client = context.Clients.Find(id);

            if (client == null)
                return View("NotFound");
            else
                return View(client);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                context.SaveChanges();
                var client = context.Clients.FirstOrDefault(c => c.ID == id);
                context.Clients.Remove(client);
                context.SaveChanges();
            }
            catch { }
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            context.Clients.Add(client);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}