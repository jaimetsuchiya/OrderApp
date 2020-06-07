using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using order.api.Domain.Commands;
using order.api.Domain.Entities;
using order.api.Infrastructure;

namespace order.api.Controllers
{
    [Route("api/[controller]")]
    public class IngredientController : BaseController<Ingredient>
    {
        private readonly IMediator _mediator;
        public IngredientController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<Ingredient>> ListAsync()
        {
            return await _mediator.Send(new ListIngredientCommand());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Ingredient), 200)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var order = await _mediator.Send(new GetIngredientCommand(id));
            if (order == null)
                return NotFound();

            return Ok(order);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Ingredient), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> PostAsync([FromBody] DTOs.CreateIngredient dto)
        {
            var response = await _mediator.Send(new CreateIngredientCommand(dto.Name, dto.Price, dto.Cost));
            return ProduceResponse(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Ingredient), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> UpdateAsync([FromBody] DTOs.UpdateIngredient dto)
        {
            var response = await _mediator.Send(new UpdateIngredientCommand(dto.IngredientId, dto.Name, dto.Price, dto.Cost, dto.Deleted));
            return ProduceResponse(response);
        }
    }
}
