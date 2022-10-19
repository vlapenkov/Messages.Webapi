using System;
using System.ComponentModel.DataAnnotations;

namespace FileStore.Domain
{
    public class FileLink : BaseEntity
    {
        [StringLength(1024)]
        public string FileName { get; set; }
        
        public Guid GlobalId { get; set; }

        [StringLength(256)]
        public string Sha256Key { get; set; }

    }
}
