using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.Controllers
{
    [Authorize(Roles = RolesNames.Admin)]
    public class CharacteristicsController : Controller
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ILogger<CharacteristicsController> _logger;
        private readonly IMapper _mapper;

        public CharacteristicsController(
            CharacteristicManager characteristicManager, 
            CategoryManager categoryManager, 
            ILogger<CharacteristicsController> logger,
            IMapper mapper)
        {
            _characteristicManager = characteristicManager;
            _categoryManager = categoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Characteristics
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryManager.GetAll();
            var characteristicViewModels = (await _characteristicManager.GetAll())
                                  .Select(characteristic => MapCharacteristic(characteristic, categories))
                                  .OrderBy(characteristic => characteristic.CategoryName);

            return View(characteristicViewModels);
        }

        // GET: Characteristics/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var categories = await _categoryManager.GetAll();
            var characteristic = MapCharacteristic(await _characteristicManager.GetById(id), categories);

            return View(characteristic);
        }

        // GET: Characteristics/Create
        public async Task<ActionResult> Create()
        {
            var characteristicViewModel = new CharacteristicViewModel
                                          {
                                              CategoriesSelectList = new SelectList(await _categoryManager.GetAll(), "Id", "Name")
                                          };

            return View(characteristicViewModel);
        }

        // POST: Characteristics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CharacteristicViewModel characteristicViewModel)
        {
            try
            {
                await _characteristicManager.Add(_mapper.Map<CharacteristicViewModel, Characteristic>(characteristicViewModel));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating characteristic. Exception: {exception.Message}");
                characteristicViewModel.CategoriesSelectList = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
                return View(characteristicViewModel);
            }
        }

        // GET: Characteristics/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var categories = await _categoryManager.GetAll();
            var characteristicViewModel = MapCharacteristic(await _characteristicManager.GetById(id),categories);
            characteristicViewModel.CategoriesSelectList = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
            return View(characteristicViewModel);
        }

        // POST: Characteristics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CharacteristicViewModel characteristicViewModel)
        {
            try
            {
                await _characteristicManager.Update(new Characteristic
                                                    {
                                                        Id = characteristicViewModel.Id,
                                                        CategoryId = characteristicViewModel.CategoryId,
                                                        Name = characteristicViewModel.Name
                                                    });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing characteristic. Exception: {exception.Message}");
                characteristicViewModel.CategoriesSelectList = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
                return View(characteristicViewModel);
            }
        }

        // GET: Characteristics/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var categories = await _categoryManager.GetAll();
            var characteristicViewModel = MapCharacteristic(await _characteristicManager.GetById(id), categories);
            return View(characteristicViewModel);
        }

        // POST: Characteristics/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CharacteristicViewModel characteristicViewModel)
        {
            try
            {
                await _characteristicManager.Delete(characteristicViewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting characteristic. Exception: {exception.Message}");
                return View(characteristicViewModel);
            }
        }

        private CharacteristicViewModel MapCharacteristic(Characteristic characteristic,IEnumerable<Category> categories)
        {
           return _mapper.Map<Characteristic, CharacteristicViewModel>(characteristic, 
                          options => options.AfterMap((src, dest) => dest.CategoryName = 
                                                                         categories.First(category => category.Id == characteristic.CategoryId).Name));
        }
    }
}