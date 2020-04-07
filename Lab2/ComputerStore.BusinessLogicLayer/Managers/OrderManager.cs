using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public class OrderManager : IManager<Order>
    {
        private readonly IRepository<OrderDto> _items;
        private readonly IMapper _mapper;
        private readonly Validator<OrderDto> _validator;

        public OrderManager(IMapper mapper, OrderValidator orderValidator, IRepository<OrderDto> items)
        {
            _mapper = mapper;
            _validator = orderValidator;
            _items = items;
        }

        public void Add(Order order)
        {
            var orderDto = _mapper.Map<OrderDto>(order);

            if (!_validator.Validate(orderDto))
            {
                throw new ValidationException($"{nameof(order)} has invalid data");
            }

            _items.Add(orderDto);
            order.Id = orderDto.Id;
        }

        public void Delete(int id)
        {
            _items.Delete(id);
        }

        public void Update(Order order)
        {
            var orderDto = _mapper.Map<OrderDto>(order);

            if (!_validator.Validate(orderDto))
            {
                throw new ValidationException($"{nameof(orderDto)} has invalid data");
            }

            _items.Update(orderDto);
        }

        public IEnumerable<Order> GetAll()
        {
            return _items.GetAll().Select(item => _mapper.Map<Order>(item));
        }

        public Order GetById(int id)
        {
            return _mapper.Map<Order>(_items.GetById(id));
        }
    }
}