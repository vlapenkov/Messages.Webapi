using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileStore.Domain
{
    public class FileData: BaseEntity
    {
        [StringLength(256)]
        public string  Sha256Key { get; set; }

        public byte[] Data { get; set; }
    }
}
