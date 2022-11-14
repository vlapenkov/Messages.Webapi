using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Interfaces.Services
{
    /// <summary>
    /// Сервис для получения данных о пользователе
    /// </summary>
    public interface IUserService
    {
        /// <summary>Идентификатор пользователя</summary>
        public Guid? UserId { get; }

        /// <summary>Имя пользователя</summary>
        string UserName { get;}

        /// <summary>Пользователь авторизован</summary>
        bool IsAuthenticated { get;}

        /// <summary>
        /// Получить значение claim по наименованию
        /// </summary>        
        string GetClaimValue(string claimName);
    }
}
