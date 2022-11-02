using Microsoft.AspNetCore.Http;
using Rk.Messages.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }               

        public string UserName => _contextAccessor.HttpContext.User.Identity.Name;

        public bool IsAuthenticated => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
