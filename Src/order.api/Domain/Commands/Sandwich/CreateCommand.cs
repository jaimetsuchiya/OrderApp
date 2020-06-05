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
    public class CreateSandwichCommand : IRequest<Response<Sandwich>>
    {
        public CreateSandwichCommand(string name, IList<SandwichIngredient> ingredients)
        {
            this.Name = name;
            this.Ingredients = ingredients;
        }

        public string Name { get; private set; }

        public IList<SandwichIngredient> Ingredients { get; private set; }
    }


    public class CreateSandwichCommandHandler : IRequestHandler<CreateSandwichCommand, Response<Sandwich>>
    {
        private readonly ISandwichRepository _repository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUIContext _appContext;

        public CreateSandwichCommandHandler(ISandwichRepository repository, IIngredientRepository ingredientRepository, IUnitOfWork unitOfWork, IAppUIContext appUIContext)
        {
            _appContext = appUIContext;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Response<Sandwich>> Handle(CreateSandwichCommand request, CancellationToken cancellationToken)
        {
            var sandwichEntity = new Sandwich() { Id = Guid.NewGuid(), Created = DateTime.Now, CreatedBy = _appContext.UserName };
            var response = await IsValid(request);

            if( response == null )
            {
                sandwichEntity.Ingredients = request.Ingredients;
                sandwichEntity.Name = request.Name;

                await _repository.AddAsync(sandwichEntity);
                await _unitOfWork.CompleteAsync();

                response = new Response<Sandwich>(sandwichEntity);
            }

            return response;
        }

        private async Task<Response<Sandwich>> IsValid(CreateSandwichCommand request)
        {
            if(string.IsNullOrEmpty(request.Name))
                return new Response<Sandwich>("Informe o nome do lanche!");

            if (request.Ingredients == null || request.Ingredients.Count == 0)
                return new Response<Sandwich>("Nenhum ingrediente informado!");

            //Atualiza os ingredientes com a posição atual do db.
            for( var i=0; i < request.Ingredients.Count; i++ )
                request.Ingredients[i].Ingredient = await _ingredientRepository.FindByIdAsync(request.Ingredients[i].IngredientId);

            if (request.Ingredients.Any(i => (i.Ingredient.Deleted.HasValue && i.Ingredient.Deleted.Value) && !i.Deleted.HasValue || !i.Deleted.Value))
                return new Response<Sandwich>($"Ingrediente {request.Ingredients.First(i => (i.Ingredient.Deleted.HasValue && i.Ingredient.Deleted.Value) && !i.Deleted.HasValue || !i.Deleted.Value).Ingredient.Name} indisponível!");

            if (request.Ingredients.Any(i => (!i.Deleted.HasValue || !i.Deleted.Value) && i.Quantity <= 0))
                return new Response<Sandwich>($"Ingrediente {request.Ingredients.First(i => (!i.Deleted.HasValue || !i.Deleted.Value) && i.Quantity <= 0).Ingredient.Name} com quantidade inválida!");

            return null;
        }
    }
}
