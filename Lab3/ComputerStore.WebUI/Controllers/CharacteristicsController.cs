using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class CharacteristicsController : Controller
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ILogger<CharacteristicsController> _logger;

        public CharacteristicsController(CharacteristicManager characteristicManager, CategoryManager categoryManager, ILogger<CharacteristicsController> logger)
        {
            _characteristicManager = characteristicManager;
            _categoryManager = categoryManager;
            _logger = logger;
        }

        // GET: Characteristics
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryManager.GetAll();
            var characteristics = (await _characteristicManager.GetAll())
                                  .Select(characteristic => CreateCharacteristicViewModel(characteristic, categories))
                                  .OrderBy(characteristic => characteristic.CategoryName);

            return View(characteristics);
        }

        // GET: Characteristics/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var categories = await _categoryManager.GetAll();
            var characteristic = CreateCharacteristicViewModel(await _characteristicManager.GetById(id), categories);

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
                await _characteristicManager.Add(new Characteristic
                                                 {
                                                     CategoryId = characteristicViewModel.CategoryId,
                                                     Name = characteristicViewModel.Name
                                                 });

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
            var characteristicViewModel = CreateCharacteristicViewModel(await _characteristicManager.GetById(id), categories);
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
            var characteristicViewModel = CreateCharacteristicViewModel(await _characteristicManager.GetById(id), categories);
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

        private CharacteristicViewModel CreateCharacteristicViewModel(Characteristic characteristic, IEnumerable<Category> categories)
        {
            return new CharacteristicViewModel
                   {
                       Id = characteristic.Id,
                       CategoryId = characteristic.CategoryId,
                       CategoryName = categories.First(category => category.Id == characteristic.CategoryId).Name,
                       Name = characteristic.Name
                   };
        }
    }
}