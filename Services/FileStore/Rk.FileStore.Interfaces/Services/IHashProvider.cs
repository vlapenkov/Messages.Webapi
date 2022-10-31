using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.FileStore.Interfaces.Services
{
    public interface IHashProvider
    {
        /// <summary>
        /// Получить хэш
        /// </summary>
        /// <param name="bytes">исходный массив байтов</param>
        /// <returns></returns>
        string GetHash(byte[] bytes);
    }
}
