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
    public class ManufacturersController : Controller
    {
        private readonly ILogger<ManufacturersController> _logger;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly IMapper _mapper;

        public ManufacturersController(ManufacturerManager manufacturerManager, 
                                       ILogger<ManufacturersController> logger, 
                                       IMapper mapper)
        {
            _manufacturerManager = manufacturerManager;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            var manufacturerViewModels = (await _manufacturerManager.GetAll()).Select(manufacturer =>
                                                                                          _mapper.Map<Manufacturer, ManufacturerViewModel>(manufacturer));

            return View(manufacturerViewModels);
        }

        // GET: Manufacturers/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var manufacturer = await _manufacturerManager.GetById(id);

            var manufacturerViewModel = _mapper.Map<Manufacturer, ManufacturerViewModel>(manufacturer);

            return View(manufacturerViewModel);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                await _manufacturerManager.Add(_mapper.Map<ManufacturerViewModel,Manufacturer>(manufacturerViewModel));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating manufacturer. Exception: {exception.Message}");
                return View(manufacturerViewModel);
            }
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var manufacturer = await _manufacturerManager.GetById(id);

            var manufacturerViewModel = _mapper.Map<Manufacturer, ManufacturerViewModel>(manufacturer);

            return View(manufacturerViewModel);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                await _manufacturerManager.Update(_mapper.Map<ManufacturerViewModel, Manufacturer>(manufacturerViewModel));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing manufacturer. Exception: {exception.Message}");
                return View(manufacturerViewModel);
            }
        }

        // GET: Manufacturers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturer = await _manufacturerManager.GetById(id);

            var manufacturerViewModel = _mapper.Map<Manufacturer, ManufacturerViewModel>(manufacturer);

            return View(manufacturerViewModel);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                await _manufacturerManager.Delete(manufacturerViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting manufacturer. Exception: {exception.Message}");
                return View(manufacturerViewModel);
            }
        }
    }
}