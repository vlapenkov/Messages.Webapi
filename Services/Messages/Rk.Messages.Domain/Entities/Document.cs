using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Документ
    /// </summary>
    public class Document :BaseEntity
    {
        public Document(string fileName,  Guid fileId)
        {
            FileName = fileName;
            
            FileId = fileId;
        }

        [Required]
        [StringLength(255)]
        public string FileName { get; private set; }
                

        [Required]
        public Guid FileId { get; private set; }
    }
}
