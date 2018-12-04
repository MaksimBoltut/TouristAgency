using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels.ServiceList;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels;
using TouristAgency.Infrastructure.Filters;
using TouristAgency.Infrastructure;

namespace TouristAgency.Controllers
{
    public class ServiceListController : Controller
    {
        private AgencyContext context;

        public ServiceListController(AgencyContext context)
        {
            this.context = context;
        }
        
        public async Task<IActionResult> Index(int? id, int page = 1, SortState sortOrder = SortState.IdAsc)
        {
            //var sessionServiceList = HttpContext.Session.Get("ServiceList");
            //if (sessionServiceList != null && id == null && page == 0 && sortOrder == SortState.IdAsc)
            //{
            //    if (sessionServiceList.Keys.Contains("id"))
            //        id = Convert.ToInt32(sessionServiceList["id"]);
            //    if (sessionServiceList.Keys.Contains("page"))
            //        page = Convert.ToInt32(sessionServiceList["page"]);
            //    if (sessionServiceList.Keys.Contains("sortOrder"))
            //        sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionServiceList["sortOrder"]);
            //}

            int pageSize = 10;

            IQueryable<ServiceList> source = context.ServiceList.Include(p => p.Service);

            if (id != null && id != 0)
            {
                source = source.Where(p => p.VoucherID == id);
            }

            switch (sortOrder)
            {
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.VoucherID);
                    break;
                case SortState.IdAsc:
                    source = source.OrderBy(s => s.VoucherID);
                    break;
                case SortState.ServiceDesc:
                    source = source.OrderByDescending(s => s.Service.Name);
                    break;
                case SortState.ServiceAsc:
                    source = source.OrderBy(s => s.Service.Name);
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
                ServiceList = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(context.ServiceList.ToList(), id)
            };
            return View(viewModel);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            ServiceList sl = context.ServiceList.Find(id);

            if (sl == null)
                return View("NotFound");
            else
                return View(sl);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var sl = context.ServiceList.Find(id);// Where(c => c.ServiceID == serviceId).Where(c=>c.VoucherID == voucherId).First();
                context.ServiceList.Remove(sl);
                context.SaveChanges();
            }
            catch { }
            return RedirectToAction("index");
        }
    }
}
