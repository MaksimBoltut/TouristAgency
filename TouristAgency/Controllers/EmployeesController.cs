﻿using TouristAgency.Models;
using TouristAgency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristAgency.ViewModels.Employees;
using TouristAgency.Infrastructure.Filters;
using TouristAgency.Infrastructure;

namespace TouristAgency.Controllers
{
    public class EmployeesController : Controller
    {
        private AgencyContext context;

        public EmployeesController(AgencyContext context)
        {
            this.context = context;
        }

        [SetToSession("Employees")]
        public async Task<IActionResult> Index(int? id, string fullname, int page = 0, SortState sortOrder = SortState.IdAsc)
        {
            var sessionEmployees = HttpContext.Session.Get("Employees");
            if (sessionEmployees != null && id == null && fullname == null && page == 0 && sortOrder == SortState.IdAsc)
            {
                if (sessionEmployees.Keys.Contains("id"))
                    id = Convert.ToInt32(sessionEmployees["id"]);
                if (sessionEmployees.Keys.Contains("fullname"))
                    fullname = sessionEmployees["fullname"];
                if (sessionEmployees.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionEmployees["page"]);
                if (sessionEmployees.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionEmployees["sortOrder"]);
            }

            if (page == 0)
                page = 1;

            int pageSize = 10;

            IQueryable<Employee> source = context.Employees;

            if (id != null && id != 0)
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
        
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Employee employee = context.Employees.Find(id);
            return View(employee);
        }
        
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            Employee employee = context.Employees.Find(id);

            if (employee == null)
                return View("NotFound");
            else
                return View(employee);
        }
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var employee = context.Employees.FirstOrDefault(c => c.ID == id);
                context.Employees.Remove(employee);
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
        public ActionResult Create(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
