using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Entities
{
    public class Document :BaseEntity
    {
        public Document(string fileName, byte[] data, Guid fileId)
        {
            FileName = fileName;
            Data = data;
            FileId = fileId;
        }

        [Required]
        [StringLength(255)]
        public string FileName { get; private set; }

        [Required]
        public byte[] Data { get; private set; }

        [Required]
        public Guid FileId { get; private set; }
    }
}
