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
    public class GetIngredientCommand : IRequest<Ingredient>
    {
        public GetIngredientCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }
    }


    public class GetIngredientCommandHandler : IRequestHandler<GetIngredientCommand, Ingredient>
    {
        private readonly IIngredientRepository _repository;

        public GetIngredientCommandHandler(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Ingredient> Handle(GetIngredientCommand request, CancellationToken cancellationToken)
        {
            return await _repository.FindByIdAsync(request.Id);
        }
    }
}
