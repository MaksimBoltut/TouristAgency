using TouristAgency.Models;
using TouristAgency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.ViewModels.Employees;

namespace TouristAgency.Controllers
{
    public class EmployeesController : Controller
    {
        private AgencyContext context;

        public EmployeesController(AgencyContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int? id, string fullname, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;

            IQueryable<Employee> source = context.Employees;

            if(id != null && id != 0)
            {
                source = source.Where(p => p.ID == id);
            }
            if (!String.IsNullOrEmpty(fullname))
            {
                source = source.Where(p => p.Fullname.Contains(fullname));
            }

            switch (sortOrder)
            {
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.ID);
                    break;
                case SortState.FullNameDesc:
                    source = source.OrderByDescending(s => s.Fullname);
                    break;
                case SortState.FullNameAsc:
                    source = source.OrderBy(s => s.Fullname);
                    break;
                case SortState.PositionDesc:
                    source = source.OrderByDescending(s => s.Position);
                    break;
                case SortState.PositionAsc:
                    source = source.OrderBy(s => s.Position);
                    break;
                case SortState.AgeDesc:
                    source = source.OrderByDescending(s => s.Age);
                    break;
                case SortState.AgeAsc:
                    source = source.OrderBy(s => s.Age);
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
                Employees = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(context.Employees.ToList(), id, fullname)
            };
            return View(viewModel);
        }
    }
}
