using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddDemo;
using Microsoft.EntityFrameworkCore;

namespace SeriesWebApp.Controllers
{
    public class SeriesController : Controller
    {
        private SeriesContext _context;

        public SeriesController(SeriesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Series.ToList());
        }

        public IActionResult Details(int id)
        {
            var serie = _context
                .Series
                .Include(s => s.Seasons)
                .ThenInclude(s => s.Episodes)
                .FirstOrDefault(s => s.Id == id);
            
            if (serie != null)
            {
                return View(serie);
            }

            return NotFound(id);
        }

        public IActionResult Edit(int id)
        {
            var serie = _context
                .Series
                .FirstOrDefault(s => s.Id == id);

            if (serie != null)
            {
                return View(serie); 
            }
            else
            {
                return NotFound(id);
            }
        }

        public IActionResult Update(int id, string title)
        {
            var serie = _context
                .Series
                .FirstOrDefault(s => s.Id == id);

            if (serie != null)
            {
                serie.Title = title;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
