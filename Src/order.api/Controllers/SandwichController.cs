using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using order.api.Domain.Commands;
using order.api.Domain.Entities;

namespace order.api.Controllers
{
    public class SandwichController : BaseController<Sandwich>
    {
        private readonly IMediator _mediator;
        public SandwichController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<Sandwich>> ListAsync()
        {
            return await _mediator.Send(new ListSandwichCommand());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Sandwich), 200)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var order = await _mediator.Send(new GetSandwichCommand(id));
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Sandwich), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> PostAsync([FromBody] DTOs.CreateSandwich dto)
        {
            var response = await _mediator.Send(new CreateSandwichCommand(dto.Name, dto.Ingredients.Select(i=> new SandwichIngredient() { 
                IngredientId = i.IngredientId,
                Quantity = i.Quantity
            }).ToList()));
            return ProduceResponse(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Sandwich), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateAsync([FromBody] DTOs.UpdateSandwich dto)
        {
            var response = await _mediator.Send(new UpdateSandwichCommand(dto.SandwichId, dto.Name, dto.Ingredients.Select(i => new SandwichIngredient()
            {
                IngredientId = i.IngredientId,
                Quantity = i.Quantity
            }).ToList(), dto.Deleted));
            return ProduceResponse(response);
        }
    }
}
