using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using order.api.Domain.Entities;
using order.api.Infrastructure;
using order.api.Repositories;

namespace order.api.Domain.Commands
{
    public class CreateOrderCommand : IRequest<Response<Order>>
    {
        public CreateOrderCommand(int tableNumber, IList<OrderSandwich> sandwiches)
        {
            this.Sandwiches = sandwiches;
            this.TableNumber = tableNumber;
        }

        public int TableNumber { get; private set; }
        public IList<OrderSandwich> Sandwiches { get; private set; }
    }


    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISandwichRepository _sandwichRepository;
        private readonly IOrderSandwichRepository _orderSandwichRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IPriceRuleRepository _priceRuleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUIContext _appContext;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IOrderSandwichRepository orderSandwichRepository, IIngredientRepository ingredientRepository, IPriceRuleRepository priceRuleRepository, ISandwichRepository sandwichRepository, IUnitOfWork unitOfWork, IAppUIContext appUIContext)
        {
            _appContext = appUIContext;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderSandwichRepository = orderSandwichRepository;
            _ingredientRepository = ingredientRepository;
            _priceRuleRepository = priceRuleRepository;
            _sandwichRepository = sandwichRepository;
        }

        public async Task<Response<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = new Order() { Id = Guid.NewGuid(), Status = Common.OrderStatusEnum.NewOrder, Rules = new List<OrderPriceRule>(), Created = DateTime.Now, CreatedBy = _appContext.UserName };
            Response<Order> response = await OrderIsValid(request);

            if( response == null )
            {
                //Calcula o valor total dos itens do pedido
                var sandwiches = OrderSandwichs(request);
                orderEntity.Sandwiches = sandwiches;
                orderEntity.TableNumber = request.TableNumber;

                decimal sandwichTotalValue = sandwiches.Select(s => s.Sandwich.Price * s.Quantity).Sum();
                decimal sandwichAdditionalIngredientsValue = sandwiches.Select(s => s.AdditionalIngredients.Where(ai => !ai.Deleted.HasValue || !ai.Deleted.Value).Select(ai => ai.Ingredient.Price * ai.Quantity).Sum()).Sum();
                decimal increaseValue = 0M;
                decimal decreaseValue = 0M;

                orderEntity.TotalItens = sandwichTotalValue + sandwichAdditionalIngredientsValue;
                orderEntity.Total = orderEntity.TotalItens;

                //Adiciona as regras para calculo do valor final
                var allRules = (await _priceRuleRepository.ListAsync()).Where(pr => !pr.Deleted.HasValue || !pr.Deleted.Value).ToList();
                decreaseValue = ApplyDecreasePriceRule(orderEntity, sandwiches, decreaseValue, allRules);
                orderEntity.Total -= decreaseValue;

                increaseValue = ApplyIncreasePriceRule(orderEntity, increaseValue, allRules);
                orderEntity.Total += increaseValue;

                await _orderRepository.AddAsync(orderEntity);
                await _unitOfWork.CompleteAsync();

                response = new Response<Order>(orderEntity);
            }

            return response;
        }

        private static decimal ApplyDecreasePriceRule(Order orderEntity, List<OrderSandwich> sandwiches, decimal decreaseValue, List<PriceRule> allRules)
        {
            var decreaseRules = allRules.Where(r => r.PriceRuleType == Common.PriceRuleTypeEnum.Discount).OrderByDescending(r => r.RuleValue).ToList();
            for (var i = 0; i < decreaseRules.Count; i++)
            {
                decimal ruleValue = 0M;
                switch (decreaseRules[i].RuleType)
                {
                    case Common.RuleTypeEnum.SandwichQuantity:
                        if (!orderEntity.Rules.Any(r => r.PriceRuleId == decreaseRules[i].Id)
                            && decreaseRules[i].RuleValue.HasValue)
                        {
                            if (decreaseRules[i].RuleValue.Value >= sandwiches.Count)
                            {
                                ruleValue = ApplyValueRule(orderEntity.Total, decreaseRules, i);
                            }
                        }
                        break;

                    case Common.RuleTypeEnum.None:
                        ruleValue = ApplyValueRule(orderEntity.Total, decreaseRules, i);
                        break;
                }

                if (ruleValue > 0M)
                {
                    decreaseValue += ruleValue;
                    orderEntity.Rules.Add(new OrderPriceRule()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderEntity.Id,
                        PriceRuleId = decreaseRules[i].Id,
                        Value = ruleValue
                    });
                }

            }

            return decreaseValue;
        }

        private static decimal ApplyIncreasePriceRule(Order orderEntity, decimal increaseValue, List<PriceRule> allRules)
        {
            var increaseRules = allRules.Where(r => r.PriceRuleType == Common.PriceRuleTypeEnum.AdditionalCharge).ToList();
            for (var i = 0; i < increaseRules.Count; i++)
            {
                decimal ruleValue = 0M;
                switch (increaseRules[i].RuleType)
                {
                    case Common.RuleTypeEnum.None:
                        ruleValue = ApplyValueRule(orderEntity.Total, increaseRules, i);
                        break;
                }

                if (ruleValue > 0M)
                {
                    increaseValue += ruleValue;
                    orderEntity.Rules.Add(new OrderPriceRule()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderEntity.Id,
                        PriceRuleId = increaseRules[i].Id,
                        Value = ruleValue
                    });
                }

            }

            return increaseValue;
        }

        private static List<OrderSandwich> OrderSandwichs(CreateOrderCommand request)
        {
            return request.Sandwiches.Where(s => !s.Deleted.HasValue || !s.Deleted.Value).ToList();
        }

        private static decimal ApplyValueRule(decimal decreaseValue, List<PriceRule> decreaseRules, int i)
        {
            switch (decreaseRules[i].PriceRuleValueType)
            {
                case Common.PriceRuleValueTypeEnum.Full:
                    decreaseValue += decreaseRules[i].Value;
                    break;

                case Common.PriceRuleValueTypeEnum.Percentage:
                    decreaseValue += ((decreaseValue * decreaseRules[i].Value) / 100);
                    break;
            }

            return decreaseValue;
        }

        private async Task<Response<Order>> OrderIsValid(CreateOrderCommand request)
        {
            if (string.IsNullOrEmpty(_appContext.UserName))
                return new Response<Order>("Responsável pela operação não informado!");

            if (request.Sandwiches == null || request.Sandwiches.Count == 0)
                return new Response<Order>("Informe ao menos um lanche para abrir o pedido!");

            //Atualiza as informações do pedido com a última posição do db
            var sandwiches = OrderSandwichs(request);
            for (var i = 0; i < sandwiches.Count; i++)
            {
                sandwiches[i].Sandwich = await _sandwichRepository.FindByIdAsync(sandwiches[i].SandwichId);
                if (sandwiches[i].AdditionalIngredients == null)
                    sandwiches[i].AdditionalIngredients = new List<OrderSandwichIngredient>();

                for ( var x =0; x < sandwiches[i].AdditionalIngredients.Count; x++)
                    sandwiches[i].AdditionalIngredients[x].Ingredient = await _ingredientRepository.FindByIdAsync(sandwiches[i].AdditionalIngredients[x].IngredientId);
            }

            if (sandwiches.Any(x => x.Sandwich.Deleted.HasValue && x.Sandwich.Deleted.Value))
                return new Response<Order>($"Lanche {sandwiches.First(x => x.Sandwich.Deleted.HasValue && x.Sandwich.Deleted.Value).Sandwich.Name} indisponível!");

            if (sandwiches.Any(x => x.AdditionalIngredients.Any(ai => !ai.Deleted.HasValue || !ai.Deleted.Value && ai.Ingredient == null || (ai.Ingredient.Deleted.HasValue && ai.Ingredient.Deleted.Value))))
            {
                var sandwich = sandwiches.First(x => x.AdditionalIngredients.Any(ai => !ai.Deleted.HasValue || !ai.Deleted.Value && ai.Ingredient == null || (ai.Ingredient.Deleted.HasValue && ai.Ingredient.Deleted.Value)));
                var ingredient = sandwich.AdditionalIngredients.First(ai => !ai.Deleted.HasValue || !ai.Deleted.Value && ai.Ingredient == null || (ai.Ingredient.Deleted.HasValue && ai.Ingredient.Deleted.Value));

                return new Response<Order>($"O ingrediente adicional {ingredient.Ingredient.Name} do Lanche {request.Sandwiches.First(x => x.Sandwich.Deleted.HasValue && x.Sandwich.Deleted.Value).Sandwich.Name}, está indisponível!");
            }

            return null;
        }
    }
}
