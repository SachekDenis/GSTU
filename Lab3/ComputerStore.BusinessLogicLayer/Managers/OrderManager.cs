using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IValidator<Order> _validator;

        public OrderManager(IMapper mapper, IValidator<Order> orderValidator, IRepository<OrderDto> items)
        {
            _mapper = mapper;
            _validator = orderValidator;
            _items = items;
        }

        public async Task Add(Order order)
        {
            var orderDto = _mapper.Map<OrderDto>(order);

            if (!_validator.Validate(order))
            {
                throw new ValidationException($"{nameof(order)} has invalid data");
            }

            await _items.Add(orderDto);
            order.Id = orderDto.Id;
        }

        public async Task Delete(int id)
        {
            await _items.Delete(id);
        }

        public async Task Update(Order order)
        {
            var orderDto = _mapper.Map<OrderDto>(order);

            if (!_validator.Validate(order))
            {
                throw new ValidationException($"{nameof(order)} has invalid data");
            }

            await _items.Update(orderDto);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return (await _items.GetAll()).Select(item => _mapper.Map<Order>(item));
        }

        public async Task<Order> GetById(int id)
        {
            return _mapper.Map<Order>(await _items.GetById(id));
        }
    }
}