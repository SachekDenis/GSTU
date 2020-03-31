using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    internal class OrderManager : IManager<OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Order> _validator;

        public OrderManager(IMapper mapper, OrderValidator orderValidator)
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

        public IEnumerable<OrderDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<OrderDto>(item));
        }
    }
}
