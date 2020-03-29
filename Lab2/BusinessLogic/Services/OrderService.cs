using AutoMapper;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Model;

namespace BusinessLogic.Services
{
    class OrderService
    {
        private readonly IMapper _mapper;
        private readonly Validator<Order> _validator;

        public OrderService(IMapper mapper, OrderValidator orderValidator)
        {
            _mapper = mapper;
            _validator = orderValidator;
        }

        public void Add(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _validator.Add(order);
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _validator.Update(order);
        }

        public IEnumerable<Order> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
