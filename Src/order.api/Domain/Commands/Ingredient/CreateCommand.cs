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
    public class CreateIngredientCommand : IRequest<Response<Ingredient>>
    {
        public CreateIngredientCommand(string name, decimal price, decimal cost)
        {
            this.Name = name;
            this.Price = price;
            this.Cost = cost;
        }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public decimal Cost { get; private set; }
    }


    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Response<Ingredient>>
    {
        private readonly IIngredientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUIContext _appContext;

        public CreateIngredientCommandHandler(IIngredientRepository repository, IUnitOfWork unitOfWork, IAppUIContext appUIContext)
        {
            _appContext = appUIContext;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<Ingredient>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientEntity = new Ingredient() { Id = Guid.NewGuid() };

            if (string.IsNullOrEmpty(_appContext.UserName))
                return new Response<Ingredient>("Responsável pela operação não informado!");

            if (ingredientEntity.Cost > ingredientEntity.Price)
                return new Response<Ingredient>("O custo do ingrediente não pode ser maior que o preço de venda!");

            ingredientEntity.Name = request.Name;
            ingredientEntity.Price = request.Price;
            ingredientEntity.Cost = request.Cost;
            ingredientEntity.Created = DateTime.Now;
            ingredientEntity.CreatedBy = _appContext.UserName;

            await _repository.AddAsync(ingredientEntity);
            await _unitOfWork.CompleteAsync();

            return new Response<Ingredient>(ingredientEntity);
        }
    }
}
