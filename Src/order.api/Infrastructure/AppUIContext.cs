using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.api.Infrastructure
{
    public interface IAppUIContext
    { 
        public string UserName { get; }
    }


    public class AppUIContext: IAppUIContext
    {
        public AppUIContext(IHttpContextAccessor httpContextAccessor)
        {
            this.UserName = httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public string UserName { get; private set; }
    }
}
