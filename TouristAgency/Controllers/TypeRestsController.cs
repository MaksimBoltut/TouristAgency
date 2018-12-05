using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels.TypeRests;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels;
using TouristAgency.Infrastructure.Filters;
using TouristAgency.Infrastructure;

namespace TouristAgency.Controllers
{
    public class TypeRestsController : Controller
    {
        private AgencyContext context;

        public TypeRestsController(AgencyContext context)
        {
            this.context = context;
        }

        [SetToSession("TypeRests")]
        public async Task<IActionResult> Index(int? id, string name, int page = 0, SortState sortOrder = SortState.IdAsc)
        {
            var sessionTypeRests = HttpContext.Session.Get("TypeRests");
            if (sessionTypeRests != null && id == null && name == null && page == 0 && sortOrder == SortState.IdAsc)
            {
                try
                {
                    if (sessionTypeRests.Keys.Contains("id"))
                        id = Convert.ToInt32(sessionTypeRests["id"]);
                    if (sessionTypeRests.Keys.Contains("name"))
                        name = sessionTypeRests["name"];
                }
                catch { }
                if (sessionTypeRests.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionTypeRests["page"]);
                if (sessionTypeRests.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionTypeRests["sortOrder"]);
            }

            if (page == 0)
                page = 1;

            int pageSize = 10;

            IQueryable<TypeRest> source = context.TypeRests;

            if (id != null && id != 0)
            {
                source = source.Where(p => p.ID == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
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
                TypeRests = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(context.TypeRests.ToList(), id, name)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            TypeRest service = context.TypeRests.Find(id);
            return View(service);
        }

        [HttpPost]
        public ActionResult Edit(TypeRest typerest)
        {
            context.TypeRests.Update(typerest);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            TypeRest typerest = context.TypeRests.Find(id);

            if (typerest == null)
                return View("NotFound");
            else
                return View(typerest);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var typerest = context.TypeRests.FirstOrDefault(c => c.ID == id);
                context.TypeRests.Remove(typerest);
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
        public ActionResult Create(TypeRest typeRest)
        {
            context.TypeRests.Add(typeRest);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}