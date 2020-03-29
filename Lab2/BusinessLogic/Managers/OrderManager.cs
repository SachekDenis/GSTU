using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    class OrderManager
    {
        private readonly IMapper _mapper;
        private readonly Validator<Order> _validator;

        public OrderManager(IMapper mapper, OrderValidator orderValidator)
        {
            _mapper = mapper;
            _validator = orderValidator;
        }

        public async Task Add(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _validator.Add(order);
        }

        public async Task Delete(int id)
        {
            await _validator.Delete(id);
        }

        public async Task Update(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _validator.Update(order);
        }

        public IEnumerable<Order> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
