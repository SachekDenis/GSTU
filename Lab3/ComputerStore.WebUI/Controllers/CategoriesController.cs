using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.AppConfiguration;
using ComputerStore.WebUI.Mappers;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.Controllers
{
    [Authorize(Roles = RolesNames.Admin)]
    public class CategoriesController : Controller
    {
        private readonly CategoryManager _categoryManager;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMapper _mapper;

        public CategoriesController(
            CategoryManager categoryManager, 
            ILogger<CategoriesController> logger, 
            IMapper mapper)
        {
            _categoryManager = categoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categoryViewModels = (await _categoryManager.GetAll()).Select(category => _mapper.Map<Category, CategoryViewModel>(category));
            return View(categoryViewModels);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryManager.GetById(id);

            var categoryViewModel = _mapper.Map<Category, CategoryViewModel>(category);

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
                await _categoryManager.Add(_mapper.Map<CategoryViewModel, Category>(categoryViewModel));

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

            var categoryViewModel = _mapper.Map<Category, CategoryViewModel>(category);

            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            try
            {
                await _categoryManager.Update(_mapper.Map<CategoryViewModel, Category>(categoryViewModel));

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

            var categoryViewModel = _mapper.Map<Category, CategoryViewModel>(category);

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
    }
}