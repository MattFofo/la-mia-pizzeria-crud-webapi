﻿using la_mia_pizzeria.DataBase;
using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {

                List<Pizza> listPizzas = context.Pizzas.ToList();

                return View(listPizzas);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}