using Rk.FileStore.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.FileStore.Interfaces.Services
{
    public interface IRemoteImageService
    {
        /// <summary>
        /// Получить данные и информацию файла
        /// </summary>
        /// <param name="requestUri">путь</param>
        /// <returns></returns>
        Task<FileDataDto> GetFile(Uri requestUri);
    }
}
