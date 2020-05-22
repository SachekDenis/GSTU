using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;

namespace ComputerStore.WebUI.Mappers
{
    public static class BuyerMapper
    {
        public static BuyerViewModel CreateBuyerViewModel(this Buyer buyer)
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