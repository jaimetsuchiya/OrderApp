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
    public class ListSandwichCommand : IRequest<IEnumerable<Sandwich>>
    {
        public ListSandwichCommand()
        {
        }
    }

    public class ListSandwichCommandHandler : IRequestHandler<ListSandwichCommand, IEnumerable<Sandwich>>
    {
        private readonly ISandwichRepository _repository;

        public ListSandwichCommandHandler(ISandwichRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Sandwich>> Handle(ListSandwichCommand request, CancellationToken cancellationToken)
        {
            var list = await _repository.ListAsync();
            var ingredients = list.First().Ingredients;
            return list;
        }
    }
}
