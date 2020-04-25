using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebUI.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly SupplierManager _supplierManager;

        public SuppliersController(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        // GET: Suppliers
        public async Task<ActionResult> Index()
        {
            var items = await _supplierManager.GetAll();
            return View(items);
        }

        // GET: Suppliers/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var item = await _supplierManager.GetById(id);
            return View(item);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Supplier supplier)
        {
            try
            {
                await _supplierManager.Add(supplier);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(supplier);
            }
        }

        // GET: Suppliers/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _supplierManager.GetById(id);
            return View(item);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Supplier supplier)
        {
            try
            {
                await _supplierManager.Update(supplier);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(supplier);
            }
        }

        // GET: Suppliers/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _supplierManager.GetById(id);
            return View(item);
        }

        // POST: Suppliers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Supplier supplier)
        {
            try
            {
                await _supplierManager.Delete(supplier.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(supplier);
            }
        }
    }
}