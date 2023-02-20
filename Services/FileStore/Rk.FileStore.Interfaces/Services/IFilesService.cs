using Rk.FileStore.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.FileStore.Interfaces.Services
{
    /// <summary>
    /// Интерфейс для загрузки и получения файлов
    /// </summary>
    public interface IFilesService
    {
        /// <summary>
        /// Создать файл
        /// </summary>        
        public  Task<Guid> CreateFile(IFileData request);

        /// <summary>
        /// Создать файлы
        /// </summary>
        Task<IReadOnlyCollection<Guid>> CreateFiles(IReadOnlyCollection<IFileData> requests);

        /// <summary>
        /// Получить содержимое файла
        /// </summary>
        Task<byte[]> GetFileContent(Guid globalId);
    }
}
