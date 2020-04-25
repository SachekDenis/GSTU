using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryManager _categoryManager;

        public CategoriesController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var items = await _categoryManager.GetAll();
            return View(items);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _categoryManager.GetById(id);
            return View(item);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category item)
        {
            try
            {
                await _categoryManager.Add(item);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(item);
            }
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _categoryManager.GetById(id);
            return View(item);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category item)
        {
            try
            {
                await _categoryManager.Update(item);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(item);
            }
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _categoryManager.GetById(id);
            return View(item);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category item)
        {
            try
            {
                await _categoryManager.Delete(item.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(item);
            }
        }
    }
}