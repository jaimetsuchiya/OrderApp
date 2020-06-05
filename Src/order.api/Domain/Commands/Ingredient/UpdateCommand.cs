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
    public class UpdateIngredientCommand : IRequest<Response<Ingredient>>
    {
        public UpdateIngredientCommand(Guid id, string name, decimal price, decimal cost, bool? deleted = null)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Cost = cost;
            this.Deleted = deleted;
        }

        public bool? Deleted { get; private set; }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public decimal Cost { get; private set; }
    }

    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, Response<Ingredient>>
    {
        private readonly IIngredientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUIContext _appContext;

        public UpdateIngredientCommandHandler(IIngredientRepository repository, IUnitOfWork unitOfWork, IAppUIContext appUIContext)
        {
            _appContext = appUIContext;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<Ingredient>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientEntity = await _repository.FindByIdAsync(request.Id);
            if(ingredientEntity == null)
                return new Response<Ingredient>("Ingrediente não encontrado!");

            if (string.IsNullOrEmpty(_appContext.UserName))
                return new Response<Ingredient>("Responsável pela operação não informado!");

            if (ingredientEntity.Cost > ingredientEntity.Price)
                return new Response<Ingredient>("O custo do ingrediente não pode ser maior que o preço de venda!");

            ingredientEntity.Name = request.Name;
            ingredientEntity.Price = request.Price;
            ingredientEntity.Cost = request.Cost;
            ingredientEntity.Deleted = request.Deleted;
            ingredientEntity.LastModifiedBy = _appContext.UserName;
            ingredientEntity.LastModified = DateTime.Now;

            if ( request.Deleted.HasValue && request.Deleted.Value)
            {
                ingredientEntity.DeletedBy = _appContext.UserName;
                ingredientEntity.DeletedAt = DateTime.Now;
            }

            _repository.Update(ingredientEntity);
            await _unitOfWork.CompleteAsync();

            return new Response<Ingredient>(ingredientEntity);
        }
    }
}
