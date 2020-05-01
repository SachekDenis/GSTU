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
            var manufacturer = await _manufacturerManager.GetById(id);
            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer manufacturer)
        {
            try
            {
                await _manufacturerManager.Add(manufacturer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(manufacturer);
            }
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var manufacturer = await _manufacturerManager.GetById(id);
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manufacturer manufacturer)
        {
            try
            {
                await _manufacturerManager.Update(manufacturer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(manufacturer);
            }
        }

        // GET: Manufacturers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturer = await _manufacturerManager.GetById(id);
            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Manufacturer manufacturer)
        {
            try
            {
                await _manufacturerManager.Delete(manufacturer.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(manufacturer);
            }
        }
    }
}