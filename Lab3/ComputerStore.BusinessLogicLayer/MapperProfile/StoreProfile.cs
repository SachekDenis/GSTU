using AutoMapper;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.MapperProfile
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Characteristic, CharacteristicDto>().ReverseMap();

            CreateMap<Field, FieldDto>().ReverseMap();

            CreateMap<Buyer, BuyerDto>().ReverseMap();
        }
    }
}