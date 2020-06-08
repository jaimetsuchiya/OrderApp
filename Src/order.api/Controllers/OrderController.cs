using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using order.api.Domain.Commands;
using order.api.Domain.Entities;
using order.api.Infrastructure;

namespace order.api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class OrderController : BaseController<Order>
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator) => _mediator = mediator;


        [HttpGet]
        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _mediator.Send(new ListOrderCommand());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), 200)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var order = await _mediator.Send(new GetOrderCommand(id));
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Order), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> PostAsync([FromBody] DTOs.CreateOrder dto)
        {
            var response = await _mediator.Send(new CreateOrderCommand(dto.TableNumber, dto.Sandwiches.Select( s => new OrderSandwich() { 
                SandwichId = s.SandwichId,
                Quantity = s.Quantity,
                Position = s.Position,
                AdditionalIngredients = s.AdditionalIngredients.Select( ai => new OrderSandwichIngredient() {
                    IngredientId = ai.IngredientId,
                    Quantity = ai.Quantity
                }).ToList()
            }).ToList()));

            return ProduceResponse(response);
        }

        [HttpPut()]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] DTOs.UpdateOrder dto)
        {
            var response = await _mediator.Send(new UpdateOrderCommand(dto.OrderId, dto.Status));
            return ProduceResponse(response);
        }

    }
}
