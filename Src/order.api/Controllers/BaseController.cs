using Microsoft.AspNetCore.Mvc;
using order.api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        protected IActionResult ProduceResponse(Response<T> response)
        {
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }
    }
}
