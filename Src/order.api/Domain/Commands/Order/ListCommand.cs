using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using order.api.Domain.Entities;
using order.api.Infrastructure;
using order.api.Repositories;

namespace order.api.Domain.Commands
{
    public class ListOrderCommand : IRequest<IEnumerable<Order>>
    {
        public ListOrderCommand()
        {
        }
    }

    public class ListOrderCommandHandler : IRequestHandler<ListOrderCommand, IEnumerable<Order>>
    {
        private readonly IOrderRepository _repository;

        public ListOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> Handle(ListOrderCommand request, CancellationToken cancellationToken)
        {
            var orders = (await _repository.ListAsync()).ToList();
            return orders;
        }
    }
}
