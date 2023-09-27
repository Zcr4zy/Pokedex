using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex_MVC_Pasta.Models;
using Pokedex.Data;
using Microsoft.EntityFrameworkCore;

namespace Pokedex_MVC_Pasta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var pokemons = _context.Pokemons.Include(p => p.Types).ThenInclude(t => t.Type).ToList();
            return View(pokemons);
        }

        public IActionResult Details(int id)
        {
            var pokemon = _context.Pokemons.Where(p => p.Id == id)
                .Include(p => p.Types).ThenInclude(t => t.Type).SingleOrDefault();
            return View(pokemon);
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
