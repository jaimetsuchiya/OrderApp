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
    public class GetSandwichCommand : IRequest<Sandwich>
    {
        public GetSandwichCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }


    public class GetSandwichCommandHandler : IRequestHandler<GetSandwichCommand, Sandwich>
    {
        private readonly ISandwichRepository _repository;

        public GetSandwichCommandHandler(ISandwichRepository repository)
        {
            _repository = repository;
        }

        public async Task<Sandwich> Handle(GetSandwichCommand request, CancellationToken cancellationToken)
        {
            return await _repository.FindByIdAsync(request.Id);
        }
    }
}
