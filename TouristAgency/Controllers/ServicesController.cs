using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels.Services;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels;

namespace TouristAgency.Controllers
{
    public class ServicesController : Controller
    {
        private AgencyContext context;

        public ServicesController(AgencyContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int? id, string name, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
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
    }
}