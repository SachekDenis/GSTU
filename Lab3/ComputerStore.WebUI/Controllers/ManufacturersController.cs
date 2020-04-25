using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebUI.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly ManufacturerManager _manufacturerManager;

        public ManufacturersController(ManufacturerManager manufacturerManager)
        {
            _manufacturerManager = manufacturerManager;
        }

        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            var manufacturers = await _manufacturerManager.GetAll();
            return View(manufacturers);
        }

        // GET: Manufacturers/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var item = await _manufacturerManager.GetById(id);
            return View(item);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer item)
        {
            try
            {
                await _manufacturerManager.Add(item);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(item);
            }
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _manufacturerManager.GetById(id);
            return View(item);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manufacturer item)
        {
            try
            {
                await _manufacturerManager.Update(item);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(item);
            }
        }

        // GET: Manufacturers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _manufacturerManager.GetById(id);
            return View(item);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Manufacturer item)
        {
            try
            {
                await _manufacturerManager.Delete(item.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(item);
            }
        }
    }
}