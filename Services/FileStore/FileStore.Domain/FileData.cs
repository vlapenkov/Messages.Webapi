using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileStore.Domain
{
    public class FileData: BaseEntity
    {
        public FileData(string sha256Key, byte[] data)
        {
            Sha256Key = sha256Key;
            Data = data;
        }
        [Required]
        [StringLength(256)]
        public string  Sha256Key { get; set; }

        public byte[] Data { get; set; }
    }
}
