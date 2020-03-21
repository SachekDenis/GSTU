using AutoMapper;
using BusinessLogic.Dto;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.MapperProfile
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<RamDto, Supply>();
            CreateMap<RamDto, Ram>();
            CreateMap<RamDto, Product>();
        }
    }
}
