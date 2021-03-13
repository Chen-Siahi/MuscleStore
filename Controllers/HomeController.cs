using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MuscleStore.Data;
using MuscleStore.Models;

namespace MuscleStore.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly MuscleStoreContext _context;

        public HomeController(ILogger<HomeController> logger, MuscleStoreContext context)
        {
            _logger = logger;
            _context = context;

        }


        public async Task<IActionResult> Index()
        {
            ICollection<Category> categories = _context.Category.ToArray();
            Dictionary<string, int> graph1 = new Dictionary<string, int>();
            Dictionary<string, int> graph2 = new Dictionary<string, int>();

            foreach (Category c in categories) {
                 graph1.Add(c.Name, (await _context.Product
                    .Where(p => p.Category.Name == c.Name)
                    .ToArrayAsync()).Count());

                graph2.Add(c.Name, (await _context.OrderDetail
                    .Include(p => p.Product)
                    .Where(p => p.Product.Category.Name == c.Name)
                    .ToArrayAsync()).Count());

            }
            ViewBag.graph1 = graph1;
            ViewBag.graph2 = graph2;
            
            return View();
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
