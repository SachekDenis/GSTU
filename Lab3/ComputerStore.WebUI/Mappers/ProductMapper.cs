using System.Collections.Generic;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Http;

namespace ComputerStore.WebUI.Mappers
{
    public static class ProductMapper
    {
        public static ProductViewModel CreateProductViewModel(this Product product, IEnumerable<Category> categories, IEnumerable<Manufacturer> manufacturers)
        {
            return new ProductViewModel
                   {
                       Id = product.Id,
                       AmountInStorage = product.AmountInStorage,
                       CategoryName = categories.First(category => category.Id == product.CategoryId).Name,
                       ManufacturerName = manufacturers.First(manufacturer => manufacturer.Id == product.ManufacturerId).Name,
                       Name = product.Name,
                       Price = product.Price,
                       CategoryId = product.CategoryId,
                       ManufacturerId = product.ManufacturerId
                   };
        }

        public static IEnumerable<FieldViewModel> CreateFieldViewModels(this Product product, IEnumerable<Characteristic> characteristics)
        {
            return product.Fields.Select(field => new FieldViewModel
                                                  {
                                                      CharacteristicId = field.CharacteristicId,
                                                      CharacteristicName = characteristics
                                                                           .First(characteristic => characteristic.Id == field.CharacteristicId)
                                                                           .Name,
                                                      Id = field.Id,
                                                      ProductId = field.ProductId,
                                                      Value = field.Value
                                                  });
        }

        public static Product CreateProductWithFields(this ProductViewModel productViewModel, IFormCollection formCollection)
        {
            var fields = formCollection.Where(formPair => formPair.Key.Contains("characteristic"))
                                       .Select(formPair => new Field
                                                           {
                                                               CharacteristicId = int.Parse(formPair.Key.Substring(formPair.Key.IndexOf('-') + 1)),
                                                               Value = formPair.Value
                                                           });
            var product = productViewModel.CreateProduct();
            product.Fields = fields;

            return product;
        }

        public static Product CreateProduct(this ProductViewModel productViewModel)
        {
            var fields = productViewModel.Fields.Select(fieldViewModel => new Field()
                                                             {
                                                                 CharacteristicId = fieldViewModel.CharacteristicId,
                                                                 Id = fieldViewModel.Id,
                                                                 ProductId = fieldViewModel.ProductId,
                                                                 Value = fieldViewModel.Value
                                                             });
            return new Product
                   {
                       AmountInStorage = productViewModel.AmountInStorage,
                       CategoryId = productViewModel.CategoryId,
                       ManufacturerId = productViewModel.ManufacturerId,
                       Name = productViewModel.Name,
                       Price = productViewModel.Price,
                       Fields = fields
                   };
        }
    }
}