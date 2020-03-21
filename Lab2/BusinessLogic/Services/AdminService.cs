using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class AdminService
    {
        private readonly RamValidator _ramValidator;
        private readonly SupplyValidator _supplyValidator;
        private readonly SupplierValidator _supplierValidator;
        private readonly ProductValidator _productValidator;

        public AdminService(RamValidator ramValidator, SupplyValidator supplyValidator, ProductValidator productValidator, SupplierValidator supplierValidator)
        {
            _ramValidator = ramValidator;
            _supplyValidator = supplyValidator;
            _productValidator = productValidator;
            _supplierValidator = supplierValidator;
        }

        public void AddRam(RamDto ram)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RamDto, Supply>()).CreateMapper();
            Supply supply = mapper.Map<Supply>(ram);
            _supplyValidator.Add(supply);
        }
    }
}
