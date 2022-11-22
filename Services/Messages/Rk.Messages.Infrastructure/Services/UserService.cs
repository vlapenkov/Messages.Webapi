using Microsoft.AspNetCore.Http;
using Rk.Messages.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private static readonly string _sub = "sub";

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserName => _contextAccessor.HttpContext.User.Identity.Name;

        public bool IsAuthenticated => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public Guid? UserId => Guid.Parse(GetClaimValue(_sub));


        /// <summary>
        /// Получить значение claim по наименованию
        /// </summary>
        /// <param name="claimName">наименование claim</param>
        /// <returns></returns>
        public string GetClaimValue(string claimName)
        {
            var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;                        

            if (identity == null) return null;
            
            var value = identity.FindFirst(claimName)?.Value;

            return value;

        }
    }
}
