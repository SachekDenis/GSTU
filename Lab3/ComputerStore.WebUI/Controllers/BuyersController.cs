using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebUI.Controllers
{
    public class BuyersController : Controller
    {
        private readonly BuyerManager _buyerManager;

        public BuyersController(BuyerManager buyerManager)
        {
            _buyerManager = buyerManager;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var buyer = await _buyerManager.GetById(id);

            var buyerViewModel = CreateBuyerViewModel(buyer);

            return View(buyerViewModel);
        }

        private BuyerViewModel CreateBuyerViewModel(Buyer buyer)
        {
            return new BuyerViewModel
                   {
                       Id = buyer.Id,
                       Address = buyer.Address,
                       Email = buyer.Email,
                       FirstName = buyer.FirstName,
                       PhoneNumber = buyer.PhoneNumber,
                       SecondName = buyer.SecondName,
                       ZipCode = buyer.ZipCode
                   };
        }
    }
}