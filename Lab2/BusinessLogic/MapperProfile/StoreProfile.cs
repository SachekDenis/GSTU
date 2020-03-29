using AutoMapper;
using BusinessLogic.Models;
using DataAccesLayer.Models;

namespace BusinessLogic.MapperProfile
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<ProductDto, Supply>();

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();

            CreateMap<ManufacturerDto, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerDto>();

            CreateMap<SupplierDto, Supplier>();
            CreateMap<Supplier, SupplierDto>();

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();

            CreateMap<CharacteristicDto, Characteristic>();
            CreateMap<Characteristic, CharacteristicDto>();

            CreateMap<SupplyDto, Supply>();
            CreateMap<Supply, SupplyDto>();
        }
    }
}
