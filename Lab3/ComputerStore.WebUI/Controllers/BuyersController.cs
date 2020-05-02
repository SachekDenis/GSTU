using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
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

            return View(buyer);
        }
    }
}