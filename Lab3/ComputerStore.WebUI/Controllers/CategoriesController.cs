using System;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryManager _categoryManager;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(CategoryManager categoryManager, ILogger<CategoriesController> logger)
        {
            _categoryManager = categoryManager;
            _logger = logger;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categoryViewModels = (await _categoryManager.GetAll()).Select(CreateCategoryViewModel);
            return View(categoryViewModels);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryManager.GetById(id);

            var categoryViewModel = CreateCategoryViewModel(category);

            return View(categoryViewModel);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            try
            {
                await _categoryManager.Add(new Category
                                           {
                                               Id = categoryViewModel.Id,
                                               Name = categoryViewModel.Name
                                           });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating category. Exception: {exception.Message}");
                return View(categoryViewModel);
            }
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryManager.GetById(id);

            var categoryViewModel = CreateCategoryViewModel(category);

            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            try
            {
                await _categoryManager.Update(new Category
                                              {
                                                  Id = categoryViewModel.Id,
                                                  Name = categoryViewModel.Name
                                              });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating category. Exception: {exception.Message}");
                return View(categoryViewModel);
            }
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryManager.GetById(id);

            var categoryViewModel = CreateCategoryViewModel(category);

            return View(categoryViewModel);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryViewModel categoryViewModel)
        {
            try
            {
                await _categoryManager.Delete(categoryViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting category. Exception: {exception.Message}");
                return View(categoryViewModel);
            }
        }

        private CategoryViewModel CreateCategoryViewModel(Category category)
        {
            return new CategoryViewModel
                   {
                       Id = category.Id,
                       Name = category.Name
                   };
        }
    }
}