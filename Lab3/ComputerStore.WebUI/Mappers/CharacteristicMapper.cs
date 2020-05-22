using System.Collections.Generic;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;

namespace ComputerStore.WebUI.Mappers
{
    public static class CharacteristicMapper
    {
        public static CharacteristicViewModel CreateCharacteristicViewModel(this Characteristic characteristic, IEnumerable<Category> categories)
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