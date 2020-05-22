using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;

namespace ComputerStore.WebUI.Mappers
{
    public static class ManufacturerMapper
    {
        public static ManufacturerViewModel CreateManufacturerViewModel(this Manufacturer manufacturer)
        {
            return new ManufacturerViewModel
                   {
                       Country = manufacturer.Country,
                       Id = manufacturer.Id,
                       Name = manufacturer.Name
                   };
        }
    }
}