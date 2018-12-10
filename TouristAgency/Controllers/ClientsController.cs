using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels.Clients;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels;
using TouristAgency.Infrastructure.Filters;
using TouristAgency.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace TouristAgency.Controllers
{
    public class ClientsController : Controller
    {
        private AgencyContext context;

        public ClientsController(AgencyContext context)
        {
            this.context = context;
        }

        [SetToSession("Clients")]
        public async Task<IActionResult> Index(int? id, string fullname, int page = 0, SortState sortOrder = SortState.IdAsc)
        {
            var sessionClients = HttpContext.Session.Get("Clients");
            if (sessionClients != null && id == null && fullname == null && page == 0 && sortOrder == SortState.IdAsc)
            {
                try
                {
                    if (sessionClients.Keys.Contains("id"))
                        id = Convert.ToInt32(sessionClients["id"]);
                    if (sessionClients.Keys.Contains("fullname"))
                        fullname = sessionClients["fullname"];
                }
                catch { }
                if (sessionClients.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionClients["page"]);
                if (sessionClients.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionClients["sortOrder"]);
            }

            if (page == 0)
                page = 1;

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

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Client client = context.Clients.Find(id);
            return View(client);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(Client client)
        {
            context.Clients.Update(client);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles ="Admin")]
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

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await context.SaveChangesAsync();
                var client = context.Clients.FirstOrDefault(c => c.ID == id);
                context.Clients.Remove(client);
                context.SaveChanges();
            }
            catch { }
            return RedirectToAction("index");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(Client client)
        {
            context.Clients.Add(client);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}