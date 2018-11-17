using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels.Hotels;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels;

namespace TouristAgency.Controllers
{
    public class HotelsController : Controller
    {
        private AgencyContext context;

        public HotelsController(AgencyContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int? id, string fullname, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;

            IQueryable<Hotel> source = context.Hotels;

            if (id != null && id != 0)
            {
                source = source.Where(p => p.ID == id);
            }
            if (!String.IsNullOrEmpty(fullname))
            {
                source = source.Where(p => p.Name.Contains(fullname));
            }

            switch (sortOrder)
            {
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.ID);
                    break;
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(s => s.Name);
                    break;
                case SortState.CountryDesc:
                    source = source.OrderByDescending(s => s.Country);
                    break;
                case SortState.CountryAsc:
                    source = source.OrderBy(s => s.Country);
                    break;
                case SortState.CityDesc:
                    source = source.OrderByDescending(s => s.City);
                    break;
                case SortState.CityAsc:
                    source = source.OrderBy(s => s.City);
                    break;
                case SortState.StarsDesc:
                    source = source.OrderByDescending(s => s.Stars);
                    break;
                case SortState.StarsAsc:
                    source = source.OrderBy(s => s.Stars);
                    break;
                case SortState.ContactFaceDesc:
                    source = source.OrderByDescending(s => s.ContactFace);
                    break;
                case SortState.ContactFaceAsc:
                    source = source.OrderBy(s => s.ContactFace);
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
                Hotels = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(context.Hotels.ToList(), id, fullname)
            };
            return View(viewModel);
        }
    }
}