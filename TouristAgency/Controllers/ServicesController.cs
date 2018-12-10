using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels.Services;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels;
using TouristAgency.Infrastructure.Filters;
using TouristAgency.Infrastructure;

namespace TouristAgency.Controllers
{
    public class ServicesController : Controller
    {
        private AgencyContext context;

        public ServicesController(AgencyContext context)
        {
            this.context = context;
        }

        [SetToSession("Services")]
        public async Task<IActionResult> Index(int? id, string name, int page = 0, SortState sortOrder = SortState.IdAsc)
        {
            var sessionService = HttpContext.Session.Get("Services");
            if (sessionService != null && id == null && name == null && page == 0 && sortOrder == SortState.IdAsc)
            {
                try
                {
                    if (sessionService.Keys.Contains("id"))
                        id = Convert.ToInt32(sessionService["id"]);
                    if (sessionService.Keys.Contains("name"))
                        name = sessionService["name"];
                }
                catch { }
                if (sessionService.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionService["page"]);
                if (sessionService.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionService["sortOrder"]);
            }

            if (page == 0)
                page = 1;

            int pageSize = 10;

            IQueryable<Service> source = context._Services;

            if (id != null && id != 0)
            {
                source = source.Where(p => p.ListID == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.ListID);
                    break;
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(s => s.Name);
                    break;
                case SortState.PriceDesc:
                    source = source.OrderByDescending(s => s.Price);
                    break;
                case SortState.PriceAsc:
                    source = source.OrderBy(s => s.Price);
                    break;

                default:
                    source = source.OrderBy(s => s.ListID);
                    break;
            }

            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Services = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(context._Services.ToList(), id, name)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Service service = context._Services.Find(id);
            return View(service);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Service service)
        {
            context._Services.Update(service);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            Service service = context._Services.Find(id);

            if (service == null)
                return View("NotFound");
            else
                return View(service);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var service = context._Services.FirstOrDefault(c => c.ListID == id);
                context._Services.Remove(service);
                await context.SaveChangesAsync();
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
        public async Task<ActionResult> Create(Service service)
        {
            context._Services.Add(service);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}