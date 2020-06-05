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
    public class ListIngredientCommand : IRequest<IEnumerable<Ingredient>>
    {
        public ListIngredientCommand()
        {
        }
    }

    public class ListIngredientCommandHandler : IRequestHandler<ListIngredientCommand, IEnumerable<Ingredient>>
    {
        private readonly IIngredientRepository _repository;

        public ListIngredientCommandHandler(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Ingredient>> Handle(ListIngredientCommand request, CancellationToken cancellationToken)
        {
            return await _repository.ListAsync();
        }
    }
}
