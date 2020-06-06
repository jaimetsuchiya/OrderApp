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
    public class SecurityController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<Order>> GuestToken()
        {
            return null;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> AdminToken()
        {
            return null;
        }
    }
}
