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
    public class GetOrderCommand : IRequest<Order>
    {
        public GetOrderCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }


    public class GetOrderCommandHandler : IRequestHandler<GetOrderCommand, Order>
    {
        private readonly IOrderRepository _repository;

        public GetOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> Handle(GetOrderCommand request, CancellationToken cancellationToken)
        {
            return await _repository.FindByIdAsync(request.Id);
        }
    }
}
