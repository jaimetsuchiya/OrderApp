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
    public class UpdateSandwichCommand : IRequest<Response<Sandwich>>
    {
        public UpdateSandwichCommand(Guid id, string name, IList<SandwichIngredient> ingredients, bool? deleted = null)
        {
            this.Id = id;
            this.Name = name;
            this.Ingredients = ingredients;
            this.Deleted = deleted;
        }

        public bool? Deleted { get; private set; }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public IList<SandwichIngredient> Ingredients { get; private set; }
    }

    public class UpdateSandwichCommandHandler : IRequestHandler<UpdateSandwichCommand, Response<Sandwich>>
    {
        private readonly ISandwichRepository _repository;
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUIContext _appContext;

        public UpdateSandwichCommandHandler(ISandwichRepository repository, IIngredientRepository ingredientRepository, IUnitOfWork unitOfWork, IAppUIContext appUIContext)
        {
            _appContext = appUIContext;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Response<Sandwich>> Handle(UpdateSandwichCommand request, CancellationToken cancellationToken)
        {
            var sandwichEntity = await _repository.FindByIdAsync(request.Id);
            var response = await IsValid(request, sandwichEntity);

            if( response == null )
            {
                sandwichEntity.Name = request.Name;
                sandwichEntity.Ingredients = request.Ingredients;
                sandwichEntity.LastModified = DateTime.Now;
                sandwichEntity.LastModifiedBy = _appContext.UserName;
                sandwichEntity.Deleted = request.Deleted;

                if( request.Deleted.HasValue && request.Deleted.Value )
                {
                    sandwichEntity.DeletedAt = DateTime.Now;
                    sandwichEntity.DeletedBy = _appContext.UserName;
                }

                _repository.Update(sandwichEntity);
                await _unitOfWork.CompleteAsync();

                response = new Response<Sandwich>(sandwichEntity);
            }
            return response;
        }

        private async Task<Response<Sandwich>> IsValid(UpdateSandwichCommand request, Sandwich sandwichEntity)
        {
            if(sandwichEntity == null)
                return new Response<Sandwich>("Lanche não encontrado!");

            if (string.IsNullOrEmpty(request.Name))
                return new Response<Sandwich>("Informe o nome do lanche!");

            if (request.Ingredients == null || request.Ingredients.Count == 0)
                return new Response<Sandwich>("Nenhum ingrediente informado!");

            //Atualiza os ingredientes com a posição atual do db.
            for (var i = 0; i < request.Ingredients.Count; i++)
                request.Ingredients[i].Ingredient = await _ingredientRepository.FindByIdAsync(request.Ingredients[i].IngredientId);

            if (request.Ingredients.Any(i => (i.Ingredient.Deleted.HasValue && i.Ingredient.Deleted.Value) && !i.Deleted.HasValue || !i.Deleted.Value))
                return new Response<Sandwich>($"Ingrediente {request.Ingredients.First(i => (i.Ingredient.Deleted.HasValue && i.Ingredient.Deleted.Value) && !i.Deleted.HasValue || !i.Deleted.Value).Ingredient.Name} indisponível!");

            if (request.Ingredients.Any(i => (!i.Deleted.HasValue || !i.Deleted.Value) && i.Quantity <= 0))
                return new Response<Sandwich>($"Ingrediente {request.Ingredients.First(i => (!i.Deleted.HasValue || !i.Deleted.Value) && i.Quantity <= 0).Ingredient.Name} com quantidade inválida!");

            return null;
        }
    }
}
