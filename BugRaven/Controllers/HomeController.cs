using BugRaven.Models;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
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

        public async Task<IActionResult> Details(string Id)
        {
            using (var session = DocumentStoreHolder.Store.OpenAsyncSession())
            {
                var model = await session.LoadAsync<Bug>(Id);
                return View(model);
            };
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            using (var session = DocumentStoreHolder.Store.OpenAsyncSession())
            {
                var model = await session.LoadAsync<Bug>(Id);
                return View(model);
            };
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Bug model)
        {
            using (var session = DocumentStoreHolder.Store.OpenAsyncSession())
            {
                var bug = await session.LoadAsync<Bug>(model.Id);
                bug.Name = model.Name;
                bug.Description = model.Description;

                await session.SaveChangesAsync();
            };

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            using (var session = DocumentStoreHolder.Store.OpenAsyncSession())
            {
                var model = await session.LoadAsync<Bug>(Id);
                return View(model);
            };
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Bug model)
        {
            using (var session = DocumentStoreHolder.Store.OpenAsyncSession())
            {
                session.Delete(model.Id);
                await session.SaveChangesAsync();
            };

            return RedirectToAction(nameof(Index));
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