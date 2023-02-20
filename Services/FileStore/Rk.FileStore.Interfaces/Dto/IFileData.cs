using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.FileStore.Interfaces.Dto
{
    public interface IFileData
    {
        /// <summary>Имя файла</summary>
        public string FileName { get; }

        /// <summary>Данные</summary>
        public byte[] Data { get; }
    }
}
