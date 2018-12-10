using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TouristAgency.Models;
using TouristAgency.ViewModels.Hotels;
using Microsoft.EntityFrameworkCore;
using TouristAgency.ViewModels;
using TouristAgency.Infrastructure.Filters;
using TouristAgency.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace TouristAgency.Controllers
{
    public class HotelsController : Controller
    {
        private AgencyContext context;
        private IHostingEnvironment appEnvironment;

        public HotelsController(AgencyContext context, IHostingEnvironment hosting)
        {
            this.context = context;
            appEnvironment = hosting;
        }

        [SetToSession("Hotels")]
        public async Task<IActionResult> Index(int? id, string fullname, int page = 0, SortState sortOrder = SortState.IdAsc)
        {
            var sessionHotels = HttpContext.Session.Get("Hotels");
            if (sessionHotels != null && id == null && fullname == null && page == 0 && sortOrder == SortState.IdAsc)
            {
                try
                {
                    if (sessionHotels.Keys.Contains("id"))
                        id = Convert.ToInt32(sessionHotels["id"]);
                    if (sessionHotels.Keys.Contains("fullname"))
                        fullname = sessionHotels["fullname"];
                }
                catch { }
                if (sessionHotels.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionHotels["page"]);
                if (sessionHotels.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionHotels["sortOrder"]);
            }

            if (page == 0)
                page = 1;

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Hotel hotel = context.Hotels.Find(id);

            if (hotel != null)
            {
                ViewBag.FotoHotel = context.Hotels.ToList();
                return View(hotel);
            }

            return RedirectToAction("Hotels");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(Hotel hotel, Microsoft.AspNetCore.Http.IFormFile photo)
        {
            if(photo != null)
            {
                string path = "/images/hotels" + DateTime.Now.ToString("ddMMyyyy") + "_" + photo.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                hotel.FotoHotel = path;
            }

            context.Hotels.Update(hotel);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            Hotel hotel = context.Hotels.Find(id);

            if (hotel == null)
                return View("NotFound");
            else
                return View(hotel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                context.SaveChanges();
                var hotel = context.Hotels.FirstOrDefault(c => c.ID == id);
                context.Hotels.Remove(hotel);
                await context.SaveChangesAsync();
            }
            catch { }
            return RedirectToAction("index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(Hotel hotel, Microsoft.AspNetCore.Http.IFormFile photo)
        {
            if (photo != null)
            {
                string path = "/images/hotels" + DateTime.Now.ToString("ddMMyyyy") + "_" + photo.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                hotel.FotoHotel = path;
            }

            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}