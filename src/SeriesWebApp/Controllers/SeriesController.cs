﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddDemo;

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
    }
}
