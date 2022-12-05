using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.FileStore.Interfaces.Services
{
    public interface IFilesService
    {
        /// <summary>
        /// Создать файл
        /// </summary>        
        public  Task<Guid> CreateFile(CreateFileRequest request);

        /// <summary>
        /// Создать файлы
        /// </summary>
        Task<IReadOnlyCollection<Guid>> CreateFiles(IReadOnlyCollection<CreateFileRequest> requests);

        /// <summary>
        /// Получить содержимое файла
        /// </summary>
        Task<byte[]> GetFileContent(Guid globalId);
    }
}
