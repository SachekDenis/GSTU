using AutoMapper;
using BusinessLogic.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers
{
    internal class OrderManager:IManager<OrderDto>
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

        public IEnumerable<OrderDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<OrderDto>(item));
        }
    }
}
