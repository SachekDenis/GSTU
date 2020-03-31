using AutoMapper;
using BusinessLogic.Models;
using DataAccessLayer.Models;

namespace BusinessLogic.MapperProfile
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<ProductDto, Supply>();

            CreateMap<ProductDto, Product>().ReverseMap();

            CreateMap<OrderDto, Order>().ReverseMap();

            CreateMap<ManufacturerDto, Manufacturer>().ReverseMap();

            CreateMap<SupplierDto, Supplier>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<CharacteristicDto, Characteristic>().ReverseMap();

            CreateMap<SupplyDto, Supply>().ReverseMap();

            CreateMap<FieldDto, Field>().ReverseMap();
        }
    }
}
