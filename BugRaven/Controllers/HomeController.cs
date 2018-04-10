using BugRaven.Models;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BugRaven.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var session = DocumentStoreHolder.Store.OpenAsyncSession())
            {
                var model = await session.Query<Bug>().ToListAsync();
                return View(model);
            };
        }

        public async Task<IActionResult> Details()
        {
            using (var session = DocumentStoreHolder.Store.OpenAsyncSession())
            {
                var model = await session.Query<Bug>().ToListAsync();
                return View(model);
            };
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Bug model)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(model);
                session.SaveChanges();
            };

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}