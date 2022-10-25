using System;
using System.ComponentModel.DataAnnotations;

namespace Rk.FileStore.Domain
{
    public class FileLink : BaseEntity
    {
        public FileLink(string fileName, string sha256Key)
        {
            FileName = fileName;
            Sha256Key = sha256Key;
            GlobalId = Guid.NewGuid();
        }

        public Guid GlobalId { get; set; }

        [StringLength(1024)]
        public string FileName { get; set; }

        [Required]
        [StringLength(256)]
        public string Sha256Key { get; set; }

    }
}
